using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MultiFilling.EventClient;

namespace MultiFilling
{
    public class NavigateArgs : EventArgs
    {
        public object Panel { get; set; }
    }

    public class FillingPageArgs : NavigateArgs {}
    public class WorklogPageArgs : NavigateArgs {}
    public class SwitchlogPageArgs : NavigateArgs {}
    public class ChangelogPageArgs : NavigateArgs {}
    public class TrendPageArgs : NavigateArgs {}
    public class FillinglogPageArgs : NavigateArgs {}

    public class NavigateTreeArgs : NavigateArgs
    {
        public string NodeName { get; set; }
    }
   
    public enum ChannelShowAs
    {
        OneChannelPage = 0,
        ChannelControllersPage = 1
    }

    public class ChannelsStatusArgs : NavigateArgs
    {
        public ChannelShowAs WhatShow { get; set; }
    }

    public enum ControllerShowAs
    {
        OneChannelPage = 0,
        OneControllerPage = 1
    }

    public class ControllersStatusArgs : NavigateArgs
    {
        public ControllerShowAs WhatShow { get; set; }
    }

    public delegate void NavigateLink(Type senderType, NavigateArgs ars);

    public static partial class Data
    {
        public static bool SystemShell { get; set; }

        public static int StartTaskPeriod { get; set; }
        public static int StopTaskPeriod { get; set; }
        public static bool ShowReadyAndAlarm { get; set; }
        public static bool UseSmartLevel { get; set; }
        public static int SmartLevelDifferent { get; set; }
        public static int SmartLevelQueueSize { get; set; }

        public static int DeleteLogsAfter { get; set; }
        public static int DeleteTrendsAfter { get; set; }

        public static event NavigateLink OnNavigate;

        private static readonly  List<EventClient.EventClient> Clients = 
            new List<EventClient.EventClient>();

        public static readonly StationNode[] StationNodes = new StationNode[4];

        public static readonly List<ChannelNode> ChannelNodes = new List<ChannelNode>();

        public static bool UserLogged { get; set; }
        public static string UserFullname { get; set; }
        public static string UserName { get; set; }
        public static UserLevel UserLevel { get; set; }

        public static readonly Dictionary<RiserAddress, RiserNode> RiserNodes = 
            new Dictionary<RiserAddress, RiserNode>();

        public static EventClient.EventClient LocalEventClient { get; set; }

        public static MemIniFile Config { get; set; }
        public static MemIniFile Session { get; set; }
        public static MemIniFile Tasks { get; set; }

        public static string ConfigFolder { get; set; }
        public static string HistoryFolder { get; set; }

        public static bool EnableLocalEventServer { get; set; }
        public static string SelfIpAddress { get; set; }

        public static bool RunningAsShell()
        {
            RegistryKey regkey;
            using (regkey = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
            {
                if (regkey != null)
                {
                    var exepath = (string)regkey.GetValue("Shell", String.Empty);
                    return exepath.Equals(Application.ExecutablePath);
                }
            }
            return false;
        }

        public static bool MustWinLogOff { get; set; }

        public static bool SetShellMode(bool shellmode, out string errormsg)
        {
            var result = true;
            errormsg = null;
            RegistryKey regkey;
            if (shellmode)
            {
                try
                {
                    using (regkey = Registry.CurrentUser.CreateSubKey(
                        "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
                    {
                        if (regkey != null) regkey.SetValue("Shell", Application.ExecutablePath);
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    errormsg = ex.Message;
                }
            }
            else
            {
                try
                {
                    using (regkey = Registry.CurrentUser.CreateSubKey(
                        "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
                    {
                        if (regkey != null) regkey.SetValue("Shell", String.Empty);
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    errormsg = ex.Message;
                }
            }
            return result;
        }

        public static bool DisableTaskManager(bool disable, out string errormsg)
        {
            var result = true;
            errormsg = null;
            RegistryKey regkey;
            if (disable)
            {
                try
                {
                    using (regkey = Registry.CurrentUser.CreateSubKey(
                        "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
                    {
                        if (regkey != null)
                        {
                            regkey.SetValue("DisableChangePassword", 1);
                            regkey.SetValue("DisableLockWorkstation", 1);
                            regkey.SetValue("DisableTaskMgr", 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    errormsg = ex.Message;
                }
            }
            else
            {
                try
                {
                    using (regkey = Registry.CurrentUser.CreateSubKey(
                        "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
                    {
                        if (regkey != null)
                        {
                            regkey.SetValue("DisableChangePassword", 0);
                            regkey.SetValue("DisableLockWorkstation", 0);
                            regkey.SetValue("DisableTaskMgr", 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    errormsg = ex.Message;
                }
            }
            return result;
        }

        public static void FetchRisers(BackgroundWorker worker, FetchArg arg)
        {
            var riserList = new List<int>();
            for (var riser = arg.RiserFirst; riser <= arg.RiserLast; riser = riser + arg.RiserStep) riserList.Add(riser);
            riserList.Add(247);
            foreach (var addr in riserList.Select(riser => new RiserAddress
                {
                    Channel = arg.Channel,
                    Overpass = arg.Overpass,
                    Way = arg.Way,
                    Product = arg.Product,
                    Riser = riser
                }))
            {
                bool riserActive;
                DateTime riserNextFetching;
                int riserChannel;
                int riserNode, riserNodeType;
                int writeaddress, readaddress, readcount, regcount;
                ushort[] writedata;

                lock (RiserNodes)
                {
                    var riser = RiserNodes[addr];
                    riserNodeType = riser.NodeType;
                    riserActive = riser.Active;
                    riserNextFetching = riser.NextFetching;
                    riserChannel = riser.Channel;
                    riserNode = riser.Node;
                    readaddress = 0;
                    readcount = 61;
                    writeaddress = riser.WriteAddress;
                    writedata = riser.WriteData;
                    riser.StartPressed = false;
                    riser.StopPressed = false;
                }
                bool channelActive;
                int sendTimeout;
                int receiveTimeout;
                int channelLinkType;
                lock (ChannelNodes)
                {
                    var channel = ChannelNodes[riserChannel];
                    channelActive = channel.Active;
                    channelLinkType = channel.LinkType;
                    sendTimeout = channel.SendTimeout;
                    receiveTimeout = channel.ReceiveTimeout;
                }
                if (!channelActive)
                {
                    worker.CancelAsync();
                    break;
                }
                if (!riserActive || riserNextFetching > DateTime.Now) continue;
                Console.WriteLine(@"Запрос: Канал {0}, контроллер {1}", riserChannel, riserNode);

                int errorcode;
                bool fetchError;
                var fetchvals = new ushort[] {};

                if (writedata != null)
                {
                    fetchError = channelLinkType == 0
                                     ? WriteTcp(arg.MoxaIp, riserNode, 16, writeaddress, writedata.Length, sendTimeout,
                                                receiveTimeout, writedata,
                                                out regcount, out errorcode)
                                     : WriteSerial(arg.Comport, arg.Baudrate, arg.Parity, riserNode, 16, 
                                     writeaddress, writedata.Length, sendTimeout, receiveTimeout, writedata,
                                                out regcount, out errorcode);
                    if (!fetchError && riserNodeType == 1 &&
                        !(new[] {0x15, 0x16, 0x17, 0x18}).Contains(writeaddress))
                    {
                        fetchError = channelLinkType == 0
                                         ? WriteTcp(arg.MoxaIp, riserNode, 16, writeaddress, 1, sendTimeout,
                                                    receiveTimeout, new ushort[] {0},
                                                    out regcount, out errorcode)
                                         : WriteSerial(arg.Comport, arg.Baudrate, arg.Parity, riserNode, 16,
                                                       writeaddress, 1, sendTimeout, receiveTimeout, new ushort[] {0},
                                                       out regcount, out errorcode);
                    }
                }
                else
                {
                    int func;
                    switch (riserNodeType)
                    {
                        case 1:
                            func = 4;
                            break;
                        default:
                            func = 3;
                            break;
                    }
                    fetchError = channelLinkType == 0
                                     ? FetchTcp(arg.MoxaIp, riserNode, func, readaddress, readcount /*61*/,
                                                sendTimeout, receiveTimeout,
                                                out regcount, out fetchvals, out errorcode)
                                     : FetchSerial(arg.Comport, arg.Baudrate, arg.Parity, riserNode, func, readaddress,
                                                   readcount /*61*/, sendTimeout, receiveTimeout,
                                                   out regcount, out fetchvals, out errorcode);

                }
                if (fetchError)
                {
                    if (errorcode == -2) // Ошибка сокета
                    {
                        lock (ChannelNodes)
                        {
                            var channel = ChannelNodes[riserChannel];
                            UpdateFetchingParams(channel);
                            channel.Linked = false;
                        }
                        //Console.WriteLine(@"Канал {0} не отвечает", riser.Channel);
                        lock (RiserNodes)
                        {
                            var riser = RiserNodes[addr];
                            UpdateFetchingParams(riser);
                            riser.UpdateRiserPropertyToRemote();
                            var linked = riser.Active && riser.BarometerValue < riser.MarginalLimit;
                            if (!linked)
                            {
                                if (riser.Linked)
                                {
                                    riser.UpdateData(new ushort[] {}, 0, 0);
                                    if (riser.Address.Channel > 0)
                                    {
                                        SendToSystemLog("Обрыв соединения", riser.Address);
                                    }
                                    riser.Linked = false;
                                }
                            }
                            riser.WriteData = null;
                        }
                    }
                    lock (RiserNodes)
                    {
                        UpdateFetchingParams(RiserNodes[addr]);
                    }
                    Console.WriteLine(@"Контроллер {0} канала {1} не отвечает", riserNode, riserChannel);
                    continue;
                }
                Console.WriteLine(@"Ответ:  Канал {0}, контроллер {1}", riserChannel, riserNode);

                int fetchtime;
                lock (ChannelNodes)
                {
                    var channel = ChannelNodes[riserChannel];
                    fetchtime = channel.FetchTime;
                    UpdateBarometer(channel);
                }
                string riserName;
                ushort[] regs = new ushort[] {}, oldregs = new ushort[] {};
                bool actived;
                lock (RiserNodes)
                {
                    var riser = RiserNodes[addr];
                    riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                              riser.Channel, riser.Overpass, riser.Way, riser.Product, riser.Riser);
                    riser.NextFetching = DateTime.Now + new TimeSpan(fetchtime*TimeSpan.TicksPerSecond);
                    riser.Status = FetchingStatus.Normal;
                    actived = riser.Actived;
                    riser.Actived = true;
                    UpdateBarometer(riser);
                    riser.UpdateRiserPropertyToRemote(); 
                    if (riser.WriteData != null)
                        riser.WriteData = null;
                    else
                    {
                        riser.UpdateData(fetchvals, readaddress, regcount);
                        regs = riser.Hregs;
                        oldregs = riser.Oldregs;
                    }
                }
                if (regs.Length <= 0 || regs.Length != oldregs.Length) continue;
                ThreadPool.QueueUserWorkItem(param =>
                    {
                        for (var i = 0; i < regs.Length; i++)
                        {
                            if (actived && regs[i] == oldregs[i]) continue;
                            var regName = "HR" + i.ToString("X2");
                            var regValue = regs[i].ToString("X4");
                            if (LocalEventClient != null)
                                LocalEventClient.UpdateProperty("Fetching", riserName, regName, regValue);
                        }
                    });
            }
        }

        public static void UpdateRiserProperty(string riserName, string propName, string propValue)
        {
            //пример построения ключа
            //riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
            //                          riser.Channel, riser.Overpass, riser.Way, riser.Product, riser.Riser);
            ThreadPool.QueueUserWorkItem(param =>
                {
                    if (LocalEventClient != null)
                        LocalEventClient.UpdateProperty("Fetching", riserName, propName, propValue);
                });
        }

        private static void UpdateBarometer(IFetchingParams item)
        {
            try
            {
                item.TotalRequests++;
                if (item.BarometerValue <= 0) return;
                if (!string.IsNullOrWhiteSpace(item.Address.Product) && item.BarometerValue >= item.MarginalLimit)
                {
                    if (!item.Linked)
                    {
                        SendToSystemLog("Установка соединения", item.Address);
                        item.Linked = true;
                    }
                }
                item.BarometerValue = 0;
            }
            catch (Exception)
            {
                item.TotalRequests = 0;
                item.TotalErrors = 0;
                item.BarometerValue = 0;
            }
        }

        private static void UpdateFetchingParams(IFetchingParams fetchingItem)
        {
            try
            {
                fetchingItem.TotalRequests++;
                fetchingItem.TotalErrors++;
            }
            catch (Exception)
            {
                fetchingItem.TotalRequests = 0;
                fetchingItem.TotalErrors = 0;
                fetchingItem.BarometerValue = 0;
            }
            if (fetchingItem.BarometerValue < fetchingItem.FailLimit)
            {
                fetchingItem.BarometerValue += 5;
                fetchingItem.Status = FetchingStatus.Normal;
            }
            else if (fetchingItem.BarometerValue >= fetchingItem.FailLimit)
            {
                fetchingItem.NextFetching = DateTime.Now + fetchingItem.TimeFail;
                fetchingItem.Status = FetchingStatus.Fail;
                fetchingItem.UpdateData(new ushort[] { }, 0, 0);
            }
            else if (fetchingItem.BarometerValue >= fetchingItem.MarginalLimit)
            {
                fetchingItem.NextFetching = DateTime.Now + fetchingItem.TimeMarginal;
                fetchingItem.Status = FetchingStatus.Marginal;
                fetchingItem.UpdateData(new ushort[] { }, 0, 0);
            }
        }

        private static byte[] EncodeData(params byte[] list)
        {
            var result = new byte[list.Length];
            for (var i = 0; i < list.Length; i++) result[i] = list[i];
            return result;
        }


        private static bool FetchSerial(int comport, int baudrate, string parityCode,
                                        int node, int func, int address, int datacount,
                                        int sendTimeout, int receiveTimeout,
                                        out int regcount, out ushort[] fetchvals, out int errorcode)
        {
            regcount = 0;
            fetchvals = new ushort[regcount];
            bool fetchError = true;
            var sendBytes = EncodeData((byte) node, (byte) (func),
                                       (byte) (address >> 8), (byte) (address & 0xff),
                                       (byte) (datacount >> 8), (byte) (datacount & 0xff), 0, 0);
            var buff = new List<byte>(sendBytes);
            var crc = BitConverter.GetBytes(Crc(buff.ToArray(), buff.Count - 2));
            sendBytes[sendBytes.Length - 2] = crc[0];
            sendBytes[sendBytes.Length - 1] = crc[1];
            errorcode = 0;
            var portname = "COM" + comport;
            var parity = Parity.None;
            switch (parityCode)
            {
                case "N": parity = Parity.None; break;
                case "E": parity = Parity.Even; break;
                case "O": parity = Parity.Odd; break;
                case "M": parity = Parity.Mark; break;
                case "S": parity = Parity.Space; break;
            }
            // Создаём последовательный порт для отправки запроса данных контроллеру
            buff.Clear();
            using (var sp = new SerialPort(portname, baudrate, parity, 8, StopBits.One))
            {
                try
                {
                    sp.WriteTimeout = sendTimeout * 1000;
                    sp.ReadTimeout = receiveTimeout * 1000;
                    sp.Open();
                    if (sp.IsOpen)
                    {
                        sp.DiscardInBuffer();
                        sp.DiscardOutBuffer();
                        sp.Write(sendBytes, 0, sendBytes.Length);
                        var len = (sendBytes[4] * 256 + sendBytes[5]) * 2 + 5;
                        while (true)
                        {
                            try
                            {
                                var onebyte = sp.ReadByte();
                                if (onebyte < 0) break; // буфер приёма пуст, ошибка
                                buff.Add((byte)onebyte);
                                if (buff.Count == len)
                                {
                                    // конец приёма блока данных
                                    break;
                                }
                            }
                            catch (TimeoutException ex)
                            {
                                // устройство не ответило вовремя, ошибка
                                fetchError = true;
                                errorcode = -2;
                                SendToErrorsLog(string.Format("Ошибка порта для [COM{0}]: {1}", comport, ex.Message));
                                buff.Clear();
                                break;
                            }
                        }
                        if (buff.Count > 0)
                        {
                            var crcCalc = Crc(buff.ToArray(), buff.Count - 2);
                            var crcBuff = BitConverter.ToUInt16(buff.ToArray(), buff.Count - 2);
                            if (crcCalc == crcBuff)
                            {
                                // данные получены правильно
                                fetchError = false;
                                errorcode = 0;
                                regcount = buff[2] / 2;
                                fetchvals = new ushort[regcount];
                                var n = 3;
                                for (var i = 0; i < regcount; i++)
                                {
                                    var raw = new byte[2];
                                    raw[0] = buff[n + 1];
                                    raw[1] = buff[n];
                                    fetchvals[i] = BitConverter.ToUInt16(raw, 0);
                                    n += 2;
                                }
                            }
                            else
                            {
                                // ошибка контрольной суммы
                                fetchError = true;
                                errorcode = -3;
                                SendToErrorsLog(string.Format("Ошибка контрольной суммы для [COM{0}] от устройства {1}", comport, node));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    fetchError = true;
                    errorcode = -1;
                    var mess = string.Format("Ошибка конфигурирования канала для [COM{0}]: {1}", comport, ex.FullMessage());
                    SendToErrorsLog(mess);
                }
            } // end of using            
                     
            return fetchError;
        }

        private static ushort Crc(IList<byte> buff, int len)
        {   // контрольная сумма MODBUS RTU
            ushort result = 0xFFFF;
            if (len <= buff.Count)
            {
                for (var i = 0; i < len; i++)
                {
                    result ^= buff[i];
                    for (var j = 0; j < 8; j++)
                    {
                        var flag = (result & 0x0001) > 0;
                        result >>= 1;
                        if (flag) result ^= 0xA001;
                    }
                }
            }
            return result;
        }

        private static bool FetchTcp(string ipaddr, int node, int func, int address, int datacount,
                              int sendTimeout, int receiveTimeout,
                              out int regcount, out ushort[] fetchvals, out int errorcode)
        {
            regcount = 0;
            fetchvals = new ushort[regcount];
            bool fetchError;
            var sendBytes = EncodeData(0, 0, 0, 0, 0, 6, (byte)node, (byte)(func),
                                       (byte)(address >> 8), (byte)(address & 0xff),
                                       (byte)(datacount >> 8), (byte)(datacount & 0xff));
            //создаём сокет для отправки запроса серверу
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                //создаём соединение                   
                try
                {
                    var remoteEp = new IPEndPoint(IPAddress.Parse(ipaddr), 502);
                    try
                    {
                        sock.Connect(remoteEp); //здесь указать нужный IP
                        //отправляем запрос на сервер
                        sock.SendTimeout = sendTimeout * 1000; // таймаут 5 секунд на запрос
                        sock.Send(sendBytes);
                        var receivedBytes = new byte[1024];
                        sock.ReceiveTimeout = receiveTimeout * 1000; // таймаут 5 секунд на ответ
                        var numBytes = sock.Receive(receivedBytes); //считали numBytes байт
                        sock.Disconnect(true);
                        // receivedBytes: [0][1][2][3][4][5] - заголовок: [4]*256+[5]= длина блока
                        // [6] - адрес устройства (как получено);
                        // [7] - код функции; [8] - количество байт ответа Modbus устройства;
                        // [9]..[n] - данные, для функции 3: [8]/2= количество регистров.
                        if ((receivedBytes[4]*256 + receivedBytes[5] == numBytes - 6) &&
                            receivedBytes[6] == node && receivedBytes[7] == func)
                        {
                            fetchError = false;
                            errorcode = 0;
                            regcount = receivedBytes[8]/2;
                            fetchvals = new ushort[regcount];
                            var n = 9;
                            for (var i = 0; i < regcount; i++)
                            {
                                var raw = new byte[2];
                                raw[0] = receivedBytes[n + 1];
                                raw[1] = receivedBytes[n];
                                fetchvals[i] = BitConverter.ToUInt16(raw, 0);
                                n += 2;
                            }
                        }
                        else if ((receivedBytes[4]*256 + receivedBytes[5] == numBytes - 6) &&
                                 receivedBytes[6] == node && receivedBytes[7] == (func | 0x80))
                        {
                            fetchError = true;
                            errorcode = receivedBytes[8];
                            SendToErrorsLog(string.Format("Ошибка связи [{0}] MODBUS устройства {1}, код: {2}", ipaddr, node, errorcode));
                        }
                        else
                        {
                            fetchError = true;
                            errorcode = -3;
                            SendToErrorsLog(string.Format("Неправильный заголовок ответа для [{0}] от устройства {1}", ipaddr, node));
                        }
                    }
                    catch (SocketException ex)
                    {
                        fetchError = true;
                        errorcode = -2;
                        SendToErrorsLog(string.Format("Ошибка сокета для [{0}]: {1}", ipaddr, ex.Message));
                    }
                    catch (Exception ex)
                    {
                        fetchError = true;
                        errorcode = -2;
                        SendToErrorsLog(string.Format("Ошибка связи для [{0}]: {1}", ipaddr, ex.FullMessage()));
                    }
                }
                catch (Exception ex)
                {
                    fetchError = true;
                    errorcode = -1;
                    SendToErrorsLog(string.Format("Ошибка конфигурирования канала для [{0}]: {1}", ipaddr, ex.FullMessage()));
                }
            }
            return fetchError;
        }

        private static ushort Swap(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            var buff = bytes[0];
            bytes[0] = bytes[1];
            bytes[1] = buff;
            return BitConverter.ToUInt16(bytes, 0);
        }

        private static bool WriteSerial(int comport, int baudrate, string parityCode, int node, int func, int address,
                                     int datacount, int sendTimeout, int receiveTimeout, IEnumerable<ushort> writevals,
                                     out int regcount, out int errorcode)
        {
            regcount = 0;
            errorcode = 0;
            bool fetchError = true;
            var list = new List<byte>();
            list.AddRange(new[] { (byte)node, (byte)(func) });
            list.AddRange(BitConverter.GetBytes(Swap((ushort)address)));
            list.AddRange(BitConverter.GetBytes(Swap((ushort)datacount)));
            list.Add((byte)(datacount * 2));
            foreach (var writeval in writevals)
                list.AddRange(BitConverter.GetBytes(Swap(writeval)));
            list.AddRange(new byte[] { 0, 0 }); // место для контрольной суммы
            var sendBytes = list.ToArray();
            var crc = BitConverter.GetBytes(Crc(list.ToArray(), list.Count - 2));
            sendBytes[sendBytes.Length - 2] = crc[0];
            sendBytes[sendBytes.Length - 1] = crc[1];
            var portname = "COM" + comport;
            var parity = Parity.None;
            switch (parityCode)
            {
                case "N": parity = Parity.None; break;
                case "E": parity = Parity.Even; break;
                case "O": parity = Parity.Odd; break;
                case "M": parity = Parity.Mark; break;
                case "S": parity = Parity.Space; break;
            }
            // Создаём последовательный порт для отправки запроса данных контроллеру
            // Создаём последовательный порт для отправки запроса данных контроллеру
            var buff = new List<byte>();
            using (var sp = new SerialPort(portname, baudrate, parity, 8, StopBits.One))
            {
                try
                {
                    sp.WriteTimeout = sendTimeout * 1000;
                    sp.ReadTimeout = receiveTimeout * 1000;
                    sp.Open();
                    if (sp.IsOpen)
                    {
                        sp.DiscardInBuffer();
                        sp.DiscardOutBuffer();
                        sp.Write(sendBytes, 0, sendBytes.Length);
                        const int len = 8;
                        while (true)
                        {
                            try
                            {
                                var onebyte = sp.ReadByte();
                                if (onebyte < 0) break; // буфер приёма пуст, ошибка
                                buff.Add((byte)onebyte);
                                if (buff.Count == len)
                                {
                                    // конец приёма блока данных
                                    break;
                                }
                            }
                            catch (TimeoutException ex)
                            {
                                // устройство не ответило вовремя, ошибка
                                fetchError = true;
                                errorcode = -2;
                                SendToErrorsLog(string.Format("Ошибка порта для [COM{0}]: {1}", comport, ex.Message));
                                buff.Clear();
                                break;
                            }
                        }
                        if (buff.Count > 0)
                        {
                            var crcCalc = Crc(buff.ToArray(), buff.Count - 2);
                            var crcBuff = BitConverter.ToUInt16(buff.ToArray(), buff.Count - 2);
                            if (crcCalc == crcBuff)
                            {
                                // данные получены правильно
                                fetchError = false;
                                errorcode = 0;
                            }
                            else
                            {
                                // ошибка контрольной суммы
                                fetchError = true;
                                errorcode = -3;
                                SendToErrorsLog(string.Format("Ошибка контрольной суммы для [COM{0}] от устройства {1}", comport, node));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    fetchError = true;
                    errorcode = -1;
                    SendToErrorsLog(string.Format("Ошибка конфигурирования канала для [COM{0}]: {1}", comport, ex.FullMessage()));
                }
            } // end of using            
 
            return fetchError;
        }

        private static bool WriteTcp(string ipAddr, int node, int func, int address,
                              int datacount, int sendTimeout, int receiveTimeout, IEnumerable<ushort> writevals,
                              out int regcount, out int errorcode)
        {
            regcount = 0;
            bool fetchError;
            var list = new List<byte>();
            list.AddRange(new byte[] { 0, 0, 0, 0, 0, (byte)(7 + datacount * 2) });
            list.AddRange(new[] { (byte)node, (byte)(func) });
            list.AddRange(BitConverter.GetBytes(Swap((ushort)address)));
            list.AddRange(BitConverter.GetBytes(Swap((ushort)datacount)));
            list.Add((byte)(datacount * 2));
            foreach (var writeval in writevals)
                list.AddRange(BitConverter.GetBytes(Swap(writeval)));
            var sendBytes = list.ToArray();
            //создаём сокет для отправки запроса серверу
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                //создаём соединение                   
                try
                {
                    var remoteEp = new IPEndPoint(IPAddress.Parse(ipAddr), 502);
                    try
                    {
                        sock.Connect(remoteEp); //здесь указать нужный IP
                        //отправляем запрос на сервер
                        sock.SendTimeout = sendTimeout; // таймаут 5 секунд на запрос
                        sock.Send(sendBytes);
                        var receivedBytes = new byte[1024];
                        sock.ReceiveTimeout = receiveTimeout; // таймаут 5 секунд на ответ
                        var numBytes = sock.Receive(receivedBytes); //считали numBytes байт
                        sock.Disconnect(true);
                        // receivedBytes: [0][1][2][3][4][5] - заголовок: [4]*256+[5]= длина блока
                        // [6] - адрес устройства (как получено); [7] - код функции;
                        // [8] - адрес первого регистра Hi байт; [9] - адрес первого регистра Lo байт;
                        // [10] - кол-во записанных рег. Hi байт; [11] - кол-во записанных рег. Lo байт;
                        if ((receivedBytes[4] * 256 + receivedBytes[5] == numBytes - 6) &&
                            receivedBytes[6] == node && receivedBytes[7] == func)
                        {
                            fetchError = false;
                            errorcode = 0;
                            //LastError = "";
                        }
                        else if ((receivedBytes[4] * 256 + receivedBytes[5] == numBytes - 6) &&
                                 receivedBytes[6] == node && receivedBytes[7] == (func | 0x80))
                        {
                            fetchError = true;
                            errorcode = receivedBytes[8];
                            //LastError = "Ошибка связи MODBUS, код: " + errorcode;
                            SendToErrorsLog("Ошибка связи MODBUS при записи, код: " + errorcode);
                        }
                        else
                        {
                            fetchError = true;
                            errorcode = -3;
                            //LastError = "Неправильный заголовок ответа от устройства";
                            SendToErrorsLog("Неправильный заголовок ответа от устройства при записи");
                        }
                    }
                    catch (Exception ex)
                    {
                        fetchError = true;
                        errorcode = -2;
                        SendToErrorsLog("Ошибка связи при записи: " + ex.FullMessage());
                    }
                }
                catch (Exception ex)
                {
                    fetchError = true;
                    errorcode = -1;
                    SendToErrorsLog("Ошибка конфигурирования при записи: " + ex.FullMessage());
                }
            }
            return fetchError;
        }

        public static void Navigate(Type senderType, NavigateArgs args)
        {
            if (OnNavigate == null) return;
            OnNavigate(senderType, args);
        }

        public static void UpdateProductTree(TreeView navigator, int displayIndex)
        {
            navigator.Nodes.Clear();
            navigator.Nodes.Add(new TreeNode("ПНВЦ") { Name = "Root" });

            lock (ChannelNodes)
            {
                foreach (var channel in ChannelNodes.OrderBy(item => item.Index))
                {
                    var nodes = navigator.Nodes.Find("Root", true);
                    if (nodes.Length == 0) return;
                    var nodeRoot = nodes[0];
                    if (channel.Overpass <= 0) continue;
                    var keyOverpass = String.Format("Overpass{0}", channel.Overpass);
                    TreeNode nodeOverpass;
                    nodes = navigator.Nodes.Find(keyOverpass, true);
                    if (nodes.Length == 0)
                    {
                        nodeOverpass = new TreeNode("Эстакада " + channel.Overpass) {Name = keyOverpass};
                        nodeRoot.Nodes.Add(nodeOverpass);
                    }
                    else
                        nodeOverpass = nodes[0];
                    var keyWay = String.Format("Overpass{0}Way{1}", channel.Overpass, channel.Way);
                    TreeNode nodeWay;
                    nodes = navigator.Nodes.Find(keyWay, true);
                    if (nodes.Length == 0)
                    {
                        nodeWay = new TreeNode("Путь " + channel.WayFine) {Name = keyWay};
                        nodeOverpass.Nodes.Add(nodeWay);
                    }
                    else
                        nodeWay = nodes[0];
                    var keyProduct = String.Format("Overpass{0}Way{1}Product{2}",
                                                   channel.Overpass, channel.Way, channel.Product);
                    TreeNode nodeProduct;
                    nodes = navigator.Nodes.Find(keyProduct, true);
                    if (nodes.Length == 0)
                    {
                        nodeProduct = new TreeNode(channel.ProductFine)
                            {
                                Name = keyProduct,
                                Tag = channel.Index.ToString("0")
                            };
                        nodeWay.Nodes.Add(nodeProduct);
                    }
                    else
                    {
                        nodeProduct = nodes[0];
                        nodeProduct.Tag = nodeProduct.Tag + ";" + channel.Index.ToString("0");
                    }
                }
            }
            var nodeKey = Config.ReadString("FillingPage" + displayIndex, "Navigate", "");
            var foundnodes = navigator.Nodes.Find(nodeKey, true);
            if (foundnodes.Length == 0) return;
            navigator.SelectedNode = foundnodes[0];          
        }

        public static string GetEnumDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(@"EnumerationValue must be of Enum type", "enumerationValue");
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute) attrs[0]).Description;
            }
            return enumerationValue.ToString();
        }

        public static void RemoteClientSendCommand(string risername, int code, ushort[] hregs)
        {
            var args = risername.Split(new[] { '_' });
            if (args.Length != 5) return;
            int channel, overpass, way, riser;
            if (!int.TryParse(args[0], out channel) || !int.TryParse(args[1], out overpass) ||
                !int.TryParse(args[2], out way) || !int.TryParse(args[4], out riser)) return;
            var product = args[3];
            lock (ChannelNodes)
            {
                if (channel < 0 || channel >= ChannelNodes.Count) return;
                // если канал на опрос не поставлен, то команда удаленного клиента пропускается для этого сервера подписки
                if (!ChannelNodes[channel].Active) return;
            }
            var addr = new RiserAddress
            {
                Channel = channel,
                Overpass = overpass,
                Way = way,
                Product = product,
                Riser = riser
            };

            var resetFilledKind = false;
            var clearFilledData = false;
            lock (RiserNodes)
            {
                if (!RiserNodes.ContainsKey(addr)) return;
                var riserNode = RiserNodes[addr];
                switch (code)
                {
                    case 0:
                        // команда очистки данных задания налива от клиента
                        if (hregs.Length == 0 && (riserNode.RiserMode == RiserState.Waiting || 
                            riserNode.RiserMode == RiserState.None))
                        {
                            riserNode.Number = "";
                            riserNode.Ntype = "";
                            riserNode.FactHeight = 0;
                            riserNode.Setpoint = 0;
                            riserNode.FilledKind = null;
                            clearFilledData = true;
                        }
                        break;
                    case 1:
                        // команда старта налива от клиента
                        if (hregs.Length == 3 && hregs[0] == 0x01)
                        {
                            if (riserNode.Ready && !riserNode.HasHandMode &&
                                riserNode.RiserMode == RiserState.Waiting &&
                                riserNode.Setpoint > 0)
                            {
                                riserNode.FilledLevel = 0;
                                riserNode.WriteAddress = 0x06;
                                riserNode.WriteData = hregs;
                                riserNode.StartPressed = true;
                                riserNode.FilledKind = null;
                                resetFilledKind = true;
                                riserNode.FillingStarted = DateTime.Now;
                                riserNode.FillingUser = Data.UserName ?? "";
                                SendToSystemLog("Запуск налива", riserNode.Address, riserNode.WaggonData);
                            }
                        }
                        break;
                    case 2:
                        // команда стопа налива от клиента
                        if (hregs.Length == 1 && hregs[0] == 0x02)
                        {
                            if (riserNode.RiserMode > RiserState.Waiting)
                            {
                                riserNode.WriteAddress = 0x06;
                                riserNode.WriteData = hregs;
                                riserNode.StopPressed = true;
                            }                        
                        }
                        break;
                }
            }
            if ((code == 1 || code == 2) && resetFilledKind)
            {
                UpdateRiserProperty(risername, "FilledLevel", "0");
                UpdateRiserProperty(risername, "FilledKind", "");
            }
            else if (code == 0 && clearFilledData)
            {
                UpdateRiserProperty(risername, "Number", "");
                UpdateRiserProperty(risername, "Ntype", "");
                UpdateRiserProperty(risername, "FactHeight", "0");
                UpdateRiserProperty(risername, "Setpoint", "0");
                UpdateRiserProperty(risername, "FilledKind", "");
                var riserAddr = string.Format("N{0}{1}{2}{3}", overpass, way, product, riser.ToString("000"));
                Tasks.EraseSection(riserAddr);
                Tasks.UpdateFile();
            }
        }

        public static void RemoteClientUpdatedRiserNode(string risername, string regname, string regvalue)
        {
            var args = risername.Split(new[] {'_'});
            if (args.Length != 5) return;
            int channel, overpass, way, riser;
            if (!int.TryParse(args[0], out channel) || !int.TryParse(args[1], out overpass) ||
                !int.TryParse(args[2], out way) || !int.TryParse(args[4], out riser)) return;
            var addr = new RiserAddress
                {
                    Channel = channel,
                    Overpass = overpass,
                    Way = way,
                    Product = args[3],
                    Riser = riser
                };
            var fp = CultureInfo.GetCultureInfo("en-US");
            lock (RiserNodes)
            {
                if (!RiserNodes.ContainsKey(addr)) return;
                var riserNode = RiserNodes[addr];
                if (regname.StartsWith("HR"))
                {
                    ushort reg, value;
                    if (!ushort.TryParse(regname.Substring(2), NumberStyles.HexNumber, fp, out reg) ||
                        !ushort.TryParse(regvalue, NumberStyles.HexNumber, fp, out value)) return;
                    riserNode.RemoteClientUpdatedRiserNodeRegister(reg, value);
                }
                else
                    riserNode.RemoteClientUpdatedRiserNodeValue(regname, regvalue);
            }

        }

        public static Guid ConnectToEventServer(string host, int port, string[] categories,
                                                PropertyUpdateWrapper propertyUpdate,
                                                ClientErrorWrapper showError,
                                                ClientFileReceivedWrapper fileReceived,
                                                ConnectionStatusWrapper connectionStatus)
        {
            var client = new EventClient.EventClient();
            Clients.Add(client);
            client.Connect(host, port, categories, propertyUpdate, showError, fileReceived, connectionStatus);
            return client.ClientId;
        }

        public static void SubscribeValues()
        {
            foreach (var eventClient in Clients)
                eventClient.SubscribeValues();
        }
    }
}
