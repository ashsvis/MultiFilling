using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Timers;
using Binding = System.ServiceModel.Channels.Binding;

namespace MultiFilling.EventClient
{
    public enum ClientConnectionStatus
    {
        Closed,
        Opening,
        Opened,
        Faulted
    }

    public class EventClient
    {
        private CallbackHandler _callback;
        private readonly Guid _clientId = Guid.NewGuid();
        private string _host;
        private int _port;
        private string[] _categories;
        private PropertyUpdateWrapper _propertyUpdate;
        private ClientErrorWrapper _showError;
        private System.Timers.Timer _faultTimer;
        private ClientFileReceivedWrapper _fileReceived;
        private ConnectionStatusWrapper _connectionStatus;

        public Guid ClientId { get { return _clientId; } }

        public void Connect(string host, int port, string[] categories, PropertyUpdateWrapper propertyUpdate,
                            ClientErrorWrapper showError, ClientFileReceivedWrapper fileReceived,
                            ConnectionStatusWrapper connectionStatus)
        {
            _host = host;
            _port = port;
            _categories = categories;
            _propertyUpdate = propertyUpdate;
            _showError = showError;
            _fileReceived = fileReceived;
            _connectionStatus = connectionStatus;
            ThreadPool.QueueUserWorkItem(param =>
                {
                    _callback = new CallbackHandler(_clientId, host, port, ConnectionStatus);
                    Thread.Sleep(100);
                    _callback.RegisterForUpdates(categories, propertyUpdate, showError, fileReceived);
                });
            _faultTimer = new System.Timers.Timer(15*1000) {AutoReset = false};
            _faultTimer.Elapsed += Reconnecting;
        }

        private void Reconnecting(object sender, ElapsedEventArgs e)
        {
            if (_showError != null)
            {
                var mess = "Попытка подключиться к серверу событий [" + _host + "] после сбоя связи.";
                _showError(mess);
            }
            Reconnect();
        }

        private void ConnectionStatus(Guid clientId, string ipaddr, ClientConnectionStatus status)
        {
            if (status == ClientConnectionStatus.Faulted)
            {
                if (_showError != null)
                {
                    var mess = "Канал связи [" + ipaddr + "] перешёл в состояние \"Ошибка\"";
                    _showError(mess);
                }
                _faultTimer.Enabled = true;
            }
            if (_connectionStatus != null) _connectionStatus(clientId, ipaddr, status);
        }

        public void UpdateProperty(string category, string pointname, string propname, string value,
                                          bool nocash = false)
        {
            if (_callback != null)
                ThreadPool.QueueUserWorkItem(param => 
                    _callback.UpdateProperty(category, pointname, propname, value, nocash));
        }

        public void SubscribeValues()
        {
            if (_callback != null) _callback.SubscribeValues();
        }

        public void Disconnect()
        {
            if (_callback != null) new Thread(() => _callback.Disconnect()).Start();
        }

        public void Reconnect(string host = null, int port = 0)
        {
            if (host != null)
            {
                _host = host;
                _port = port;
            }
            ThreadPool.QueueUserWorkItem(param =>
                {
                    Disconnect();
                    Thread.Sleep(500);
                    Connect(_host, _port, _categories, _propertyUpdate, _showError, 
                        _fileReceived, _connectionStatus);
                });
        }

        /// <summary>Запрос клиентом файла на сервере</summary>
        /// <param name="source">полное имя файла для чтения на сервере</param>
        /// <param name="target">полное имя файла для записи на клиенте</param>
        public void GetFile(string source, string target)
        {
            if (_callback != null)
            {
                _callback.GetFile(source, target);
            }
        }

        public void SendCommand(string address, int command, ushort[] hregs)
        {
            if (_callback != null)
            {
                _callback.SendCommand(address, command, hregs);
            }
        }
    }

    public delegate void PropertyUpdateWrapper(
        DateTime servertime, string category, string pointname, string propname, string value);

    public delegate void ClientErrorWrapper(string errormessage);

    public delegate void ConnectionStatusWrapper(Guid clientId, string ipaddr, ClientConnectionStatus status);

    public delegate void ClientFileReceivedWrapper(string tarfilename, int percent, bool complete);

    public class CallbackHandler : DataServiceRef.IAShEventServiceCallback, IDisposable
    {
        private readonly Guid _clientId;
        private readonly string _host;
        private readonly TimeSpan _timeout = new TimeSpan(0, 1, 30);
        private readonly InstanceContext _site;
        private readonly Binding _binding;
        private readonly ConnectionStatusWrapper _connectionStatus;
        private readonly DataServiceRef.AShEventServiceClient _proxy;
        private readonly ConcurrentDictionary<Guid, FileItem> _cashfiles =
            new ConcurrentDictionary<Guid, FileItem>();

        public CallbackHandler(Guid clientId, string host, int port, ConnectionStatusWrapper connectionStatus)
        {
            _clientId = clientId;
            _host = host;
            _connectionStatus = connectionStatus;
            var uri = (host == null || host.Trim().Length == 0 || host.ToLower().Equals("localhost") ||
                       host.Equals("127.0.0.1"))
                          ? "net.pipe://localhost/FillingEventServer"
                          : String.Format("net.tcp://{0}:{1}/FillingEventServer", host.Trim(), port);
            _site = new InstanceContext(this);
            if (uri.StartsWith("net.tcp://"))
            {
                _binding = new NetTcpBinding
                {
                    OpenTimeout = _timeout,
                    SendTimeout = _timeout,
                    ReceiveTimeout = _timeout,
                    CloseTimeout = _timeout,
                    Security = new NetTcpSecurity { Mode = SecurityMode.None }
                };
            }
            else
                _binding = new NetNamedPipeBinding
                {
                    OpenTimeout = _timeout,
                    SendTimeout = _timeout,
                    ReceiveTimeout = _timeout,
                    CloseTimeout = _timeout
                };
            _proxy = new DataServiceRef.AShEventServiceClient(_site, _binding, new EndpointAddress(uri));
            
            _proxy.InnerDuplexChannel.Opened += (sender, args) =>
                {
                    if (_connectionStatus != null) _connectionStatus(_clientId, _host, ClientConnectionStatus.Opened);
                    ThreadPool.QueueUserWorkItem(arg =>
                        {
                            Thread.Sleep(1000);
                            SubscribeValues();
                        });
                };
            _proxy.InnerDuplexChannel.Opening += (sender, args) =>
                {
                    if (_connectionStatus != null) _connectionStatus(_clientId, _host, ClientConnectionStatus.Opening);
                };
            _proxy.InnerDuplexChannel.Closed += (sender, args) =>
                {
                    if (_connectionStatus != null) _connectionStatus(_clientId, _host, ClientConnectionStatus.Closed);
                };
            
            _proxy.InnerDuplexChannel.Faulted += (sender, args) =>
                {
                    if (_connectionStatus != null) _connectionStatus(_clientId, _host, ClientConnectionStatus.Faulted);
                };
        }

        private PropertyUpdateWrapper _propertyUpdate;
        private ClientErrorWrapper _showError;
        private ClientFileReceivedWrapper _fileReceived;

        /// <summary>Регистрирует клиента на подписку</summary>
        /// <param name="categories">строковый массив категорий для подписки</param>
        /// <param name="propertyUpdate">делегат события при изменении значения свойства</param>
        /// <param name="showError">делегат события при ошибке</param>
        /// <param name="fileReceived">делегат события при получении файла</param>
        public bool RegisterForUpdates(string[] categories, PropertyUpdateWrapper propertyUpdate,
                                       ClientErrorWrapper showError = null,
                                       ClientFileReceivedWrapper fileReceived = null)
        {
            _propertyUpdate = propertyUpdate;
            _showError = showError;
            _fileReceived = fileReceived;
            try
            {
                _proxy.RegisterForUpdates(_clientId, categories);
                return true;
            }
            catch (EndpointNotFoundException ex)
            {
                Data.SendToErrorsLog("Ошибка подключения к [" + _host + "]: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в RegisterForUpdates() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в RegisterForUpdates() для [" + _host + "]: " + ex.FullMessage());
                return false;
            }
        }

        /// <summary>Рассылка всех значений из накопленного кэша сервера вновь подключившемуся клиенту</summary>
        public void SubscribeValues()
        {
            try
            {
                if (_proxy.State.Equals(CommunicationState.Opened))
                    _proxy.SubscribeValues(_clientId);
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в SubscribeValues() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в SubscribeValues() для [" + _host + "]: " + ex.FullMessage());
            }
        }

        /// <summary>Изменение значения свойства клиентом</summary>
        /// <param name="category">имя категории</param>
        /// <param name="pointname">имя объекта</param>
        /// <param name="propname">имя свойства</param>
        /// <param name="value">значение</param>
        /// <param name="nocash">не запоминать в кеш сервера</param>
        public void UpdateProperty(string category, string pointname, string propname, string value, bool nocash)
        {
            try
            {
                if (_proxy.State == CommunicationState.Opened)
                    _proxy.UpdateProperty(_clientId, category, pointname, propname, value, nocash);
            }
            catch (CommunicationObjectFaultedException ex)
            {
                Data.SendToErrorsLog("Ошибка в UpdateProperty() для [" + _host + "]: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                Data.SendToErrorsLog("Ошибка в UpdateProperty() для [" + _host + "]: " + ex.Message);
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в UpdateProperty() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в UpdateProperty() для [" + _host + "]: " + ex.FullMessage());
            }
        }

        private void SendMessage(string message)
        {
            if (_showError == null) return;
            try
            {
                _showError(message);
            }
            catch (Exception ex)
            {
                Data.SendToErrorsLog("Ошибка при выводе сообщения: " + ex.FullMessage());
            }
        }

        public void PropertyUpdated(DateTime servertime, string category, string pointname, string propname,
                                    string value)
        {
            if (_propertyUpdate == null) return;
            try
            {
                _propertyUpdate(servertime, category, pointname, propname, value);
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в PropertyUpdated() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в PropertyUpdated() для [" + _host + "]: " + ex.FullMessage());
            }
        }

        public void SendCommand(string address, int command, ushort[] hregs)
        {
            try
            {
                if (_proxy.State == CommunicationState.Opened)
                    _proxy.SendCommand(_clientId, address, command, hregs);
            }
            catch (CommunicationObjectFaultedException ex)
            {
                Data.SendToErrorsLog("Ошибка в SendCommand() для [" + _host + "]: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                Data.SendToErrorsLog("Ошибка в SendCommand() для [" + _host + "]: " + ex.Message);
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в SendCommand() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в SendCommand() для [" + _host + "]: " + ex.FullMessage());
            }
            
        }


        /// <summary>Запрос клиентом файла на сервере</summary>
        /// <param name="source">полное имя файла для чтения на сервере</param>
        /// <param name="target">полное имя файла для записи на клиенте</param>
        public void GetFile(string source, string target)
        {
            var fileId = Guid.NewGuid();
            try
            {
                if (!_proxy.State.Equals(CommunicationState.Opened)) return;
                _oldpercent = -1;
                var tmpname = Path.Combine(Path.GetDirectoryName(target) ?? "", fileId.ToString());
                var item = new FileItem
                {
                    Length = 0,
                    SourceFileName = source,
                    TargetFileName = target,
                    TempFileName = tmpname,
                    FileStream = File.Create(tmpname)
                };
                _cashfiles.TryAdd(fileId, item);
                _proxy.GetFile(_clientId, source, fileId);
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в GetFile() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в GetFile() для [" + _host + "]: " + ex.FullMessage());
            }
        }

        private int _oldpercent;

        public void FileBlockReceived(Guid fileId, long length, int block, int size, byte[] buffer, byte[] md5)
        {
            FileItem item;
            if (!_cashfiles.TryGetValue(fileId, out item)) return;
            var newitem = new FileItem
            {
                Length = item.Length += size,
                SourceFileName = item.SourceFileName,
                TargetFileName = item.TargetFileName,
                TempFileName = item.TempFileName,
                FileStream = item.FileStream,
            };
            _cashfiles.TryUpdate(fileId, item, newitem);
            if (item.FileStream == null) return;
            if (!_proxy.State.Equals(CommunicationState.Opened)) return;
            item.FileStream.Write(buffer, 0, size);
            var percent = Convert.ToInt32(100.0 * item.Length / length);
            if (_fileReceived != null && percent != _oldpercent)
            {
                try
                {
                    _fileReceived(item.TargetFileName, percent, false);
                }
                catch (Exception ex)
                {
                    var message = String.Concat("Ошибка в FileBlockReceived() для [" + _host + "]: ", ex.Message);
                    SendMessage(message);
                    Data.SendToErrorsLog("Ошибка в FileBlockReceived() для [" + _host + "]: " + ex.FullMessage());
                }
                _oldpercent = percent;
            }
            if (!item.Length.Equals(length)) return;
            // Файл получен полностью
            item.FileStream.Position = 0;
            var md5Hasher = MD5.Create();
            var md5Data = md5Hasher.ComputeHash(item.FileStream);
            item.FileStream.Close();
            try
            {
                //SendMessage(Md5ToString(md5));
                //SendMessage(Md5ToString(md5Data));
                if (Md5ToString(md5) == Md5ToString(md5Data))
                {
                    var unzipname = Guid.NewGuid().ToString();
                    using (var inFile = File.OpenRead(item.TempFileName))
                    {
                        using (var outFile = File.Create(unzipname))
                        {
                            using (var decompress = new GZipStream(inFile, CompressionMode.Decompress))
                            {
                                decompress.CopyTo(outFile);
                            }
                        }
                    }
                    if (File.Exists(item.TargetFileName)) File.Delete(item.TargetFileName);
                    File.Move(unzipname, item.TargetFileName);
                    if (_fileReceived != null) _fileReceived(item.TargetFileName, 100, true);
                }
                else
                {
                    if (_fileReceived != null) _fileReceived(item.TargetFileName, 0, true);
                }
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в FileBlockReceived() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                if (_fileReceived != null) 
                    _fileReceived(item.TargetFileName, 0, true);
                Data.SendToErrorsLog("Ошибка в FileBlockReceived() для [" + _host + "]: " + ex.FullMessage());
            }
            finally
            {
                File.Delete(item.TempFileName);
            }
            _cashfiles.TryRemove(fileId, out item);
        }

        private static string Md5ToString(byte[] data)
        {
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public void Disconnect()
        {
            try
            {
                if (!_proxy.State.Equals(CommunicationState.Opened)) return;
                DisposeTempFiles();
                _proxy.Disconnect(_clientId);
                _proxy.InnerDuplexChannel.Close();
            }
            catch (Exception ex)
            {
                var message = String.Concat("Ошибка в Disconnect() для [" + _host + "]: ", ex.Message);
                SendMessage(message);
                Data.SendToErrorsLog("Ошибка в Disconnect() для [" + _host + "]: " + ex.FullMessage());
            }
        }

        private void DisposeTempFiles()
        {
            foreach (var fileGuid in _cashfiles.Keys)
            {
                FileItem item;
                if (!_cashfiles.TryGetValue(fileGuid, out item)) continue;
                if (item.FileStream != null) item.FileStream.Close();
                if (File.Exists(item.TempFileName)) File.Delete(item.TempFileName);
            }
        }

        public void Dispose()
        {
            DisposeTempFiles();
        }
    }

    class FileItem
    {
        public long Length { get; set; }
        public string SourceFileName { get; set; }
        public string TargetFileName { get; set; }
        public string TempFileName { get; set; }
        public FileStream FileStream { get; set; }
    }

}
