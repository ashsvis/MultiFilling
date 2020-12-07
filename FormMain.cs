using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using MultiFilling.EventClient;
using MultiFilling.EventServer;
using MultiFilling.TypesList;
using MultiFilling.UserList;

namespace MultiFilling
{
    public partial class FormMain : Form, IUserInfo
    {
        private WcfEventService _wcfEventService;
        private FormPanel[] _panels;
        private static readonly List<BackgroundWorker> Workers = new List<BackgroundWorker>();
        private FileSystemWatcher _fileLogsWatcher;
        private readonly System.Windows.Forms.Timer _timer;
        private int _lastMinute;
        private int _lastHour;
        private int _lastDay;

        public FormMain()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            Load += FormMain_Load;
            FormClosing += FormMain_FormClosing;

            _timer = new System.Windows.Forms.Timer {Interval = 1000};
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            #region очистка старых данных
            var now = DateTime.Now;
            var dd = now.Day;
            var hh = now.Hour;
            var mm = now.Minute;
            if (_lastMinute == mm) return;
            _lastMinute = mm;
            if (_lastHour == hh) return;
            _lastHour = hh;
            if (_lastDay == dd) return;
            _lastDay = dd;
            _timer.Enabled = false;
            ThreadPool.QueueUserWorkItem(arg =>
                {
                    // очистка накоплений
                    if (Data.DeleteLogsAfter > 0)
                        Data.DeleteFromLogs(now.AddDays(-Data.DeleteLogsAfter));
                    if (Data.DeleteTrendsAfter > 0)
                        Data.DeleteFromTrends(now.AddDays(-Data.DeleteTrendsAfter));

                    var method = new MethodInvoker(() =>
                        {
                            _timer.Enabled = true;
                        });
                    if (InvokeRequired)
                        BeginInvoke(method);
                    else
                        method();
                });

            #endregion
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            #region Защита от повторного запуска

            var process = RunningInstance();
            if (process != null) { Application.Exit(); return; }

            #endregion

            Width = 0; Height = 1;

            Data.Session = new MemIniFile("");

            Data.FileToErrorsFullName = Path.ChangeExtension(Application.ExecutablePath, ".err");
            Data.ResetErrorsLog();

            var tasksFileName = Path.ChangeExtension(Application.ExecutablePath, ".tsk");
            Data.Tasks = new MemIniFile(tasksFileName);

            var configFileName = Path.ChangeExtension(Application.ExecutablePath, ".ini");
            Data.Config = new MemIniFile(configFileName);
            var currentPath = Application.StartupPath;

            var configFolder = Path.Combine(currentPath, "MultiFillingConfig");
            if (!Directory.Exists(configFolder))
            {
                try
                {
                    Directory.CreateDirectory(configFolder);
                }
                catch (Exception ex)
                {
                    Data.SendToErrorsLog("Ошибка при создании папки " + configFolder + ": " +ex.FullMessage());
                    Application.Exit();
                    return;
                }
            }
            Data.ConfigFolder = configFolder;

            #region импорт файла типов вагонов

            //var typeFile = Path.Combine(configFolder, "waggontypes.txt");
            //if (File.Exists(typeFile))
            //{
            //    TypesListKeeper.Clear();
            //    var n = 0;
            //    foreach (var vals in File.ReadLines(typeFile).Select(line => line.Split(new[] { '\t' })))
            //    {
            //        if (n > 0 && vals.Length == 5)
            //        {
            //            int diameter, throat, deflevel;
            //            if (!string.IsNullOrWhiteSpace(vals[0]) &&
            //                int.TryParse(vals[1], out diameter) &&
            //                int.TryParse(vals[2], out throat) &&
            //                int.TryParse(vals[3], out deflevel))
            //            {
            //                TypesListKeeper.Add(vals[0], diameter, throat, deflevel);
            //            }
            //        }
            //        n++;
            //    }
            //    typeFile = Path.Combine(configFolder, "wagtypes.xml");
            //    TypesListKeeper.SaveTypesData(typeFile);
            //}

            #endregion

            TypeDataKeeper.LoadTypesData(Path.Combine(configFolder, "wagtypes.xml"));

            #region импорт и экспорт файла вагонов

            //var wagFile = Path.Combine(configFolder, "_waggons.txt");
            //if (File.Exists(wagFile))
            //{
            //    var filename = Path.Combine(configFolder, "waggons.txt");
            //    using (var stream = new FileStream(filename, FileMode.Append))
            //    {
            //        using (var writer = new StreamWriter(stream))
            //        {
            //            var n = 0;
            //            foreach (var vals in File.ReadLines(wagFile).Select(line => line.Split(new[] {'\t'})))
            //            {
            //                if (n > 0 && vals.Length == 5)
            //                {
            //                    var number = vals[0];
            //                    var ntype = vals[1];
            //                    int factlevel;
            //                    if (!string.IsNullOrWhiteSpace(number) &&
            //                        !string.IsNullOrWhiteSpace(ntype) &&
            //                        int.TryParse(vals[2], out factlevel))
            //                    {
            //                        var content = string.Format("{0}\t{1}\t{2}", number, ntype, factlevel);
            //                        writer.WriteLine(content);
            //                        writer.Flush();
            //                    }
            //                }
            //                n++;
            //            }
            //        }
            //    }
            //}

            #endregion

            var logFolder = Path.Combine(currentPath, "MultiFillingLogs");
            if (!Directory.Exists(logFolder))
            {
                try
                {
                    Directory.CreateDirectory(logFolder);
                }
                catch (Exception ex)
                {
                    Data.SendToErrorsLog("Ошибка при создании папки " + logFolder + ": " + ex.FullMessage());
                    Application.Exit();
                    return;
                }
            }
            Data.LogsFolder = logFolder;

            Data.SendToSystemLog(string.Format("Запуск системы на станции {0}", Environment.MachineName));
            var dt = DateTime.Now + new TimeSpan(0, 0, 0, 3);
            while (dt > DateTime.Now) Application.DoEvents();

            _fileLogsWatcher = new FileSystemWatcher
                {
                    Path = logFolder,
                    EnableRaisingEvents = true,
                    Filter = "*.log",
                    NotifyFilter = NotifyFilters.LastWrite,
                    SynchronizingObject = this
                };
            _fileLogsWatcher.Changed += fileSystemLogWatcher_Changed;
            _fileLogsWatcher.Created += fileSystemLogWatcher_Changed;


            var historyFolder = Path.Combine(currentPath, "MultiFillingHistory");
            if (!Directory.Exists(historyFolder))
            {
                try
                {
                    Directory.CreateDirectory(historyFolder);
                }
                catch (Exception ex)
                {
                    Data.SendToErrorsLog("Ошибка при создании папки " + historyFolder + ": " + ex.FullMessage());
                    Application.Exit();
                    return;
                }
            }
            Data.HistoryFolder = historyFolder;

            Data.SystemShell = Data.Config.ReadBool("GeneralFor" + Environment.UserName, "SystemShell", false);
            Data.StartTaskPeriod  = Data.Config.ReadInteger("General", "StartTaskPeriod", 3000);
            Data.StopTaskPeriod = Data.Config.ReadInteger("General", "StopTaskPeriod", 3000);
            Data.DeleteLogsAfter = Data.Config.ReadInteger("General", "DeleteLogsAfter", 90);
            Data.DeleteTrendsAfter = Data.Config.ReadInteger("General", "DeleteTrendsAfter", 14);

            Data.ShowReadyAndAlarm = Data.Config.ReadBool("FillingPageCommon", "ShowReadyAndAlarm", false);
            Data.UseSmartLevel = Data.Config.ReadBool("FillingPageCommon", "UseSmartLevel", true);
            Data.SmartLevelDifferent = Data.Config.ReadInteger("FillingPageCommon", "SmartLevelDifferent", 3);
            Data.SmartLevelQueueSize = Data.Config.ReadInteger("FillingPageCommon", "SmartLevelQueueSize", 15);

            // Загрузка конфигурации опроса
            try
            {
                LoadFetchConfiguration();
            }
            catch (Exception ex)
            {
                Data.SendToErrorsLog("Ошибка при загрузке конфигурации опроса: " + ex.FullMessage());
            }

            #region Построение рабочих панелей

            var monitors = Screen.AllScreens;

            _panels = new FormPanel[monitors.Length];

            if (!Data.Config.KeyExists("Station", "Monitors"))
            {
                Data.Config.WriteInteger("Station", "Monitors", 1);
                Data.Config.UpdateFile();
            }
            var screens = Data.Config.ReadInteger("Station", "Monitors", 1);

            for (var i = 0; i < monitors.Length; i++)
            {
                _panels[i] = new FormPanel(this, monitors[i].Primary, monitors[i].WorkingArea)
                    {
                        Visible = false,
                        DisplayIndex = i + 1
                    };
                _panels[i].Show(this);
                _panels[i].Refresh();
                if (screens > 0 && i + 1 >= screens)
                    break;
            }

            #endregion

            Data.StationNodes[0] = new StationNode { Name = "Станция 1", Index = 1 };
            Data.StationNodes[1] = new StationNode { Name = "Станция 2", Index = 2 };
            Data.StationNodes[2] = new StationNode { Name = "Станция 3", Index = 3 };
            Data.StationNodes[3] = new StationNode { Name = "Станция 4", Index = 4 };

            Data.EnableLocalEventServer = Data.Config.ReadBool("DataEventServers", "EnableLocalEventServer", false);
            
            // Запуск сервера событий
            if (Data.EnableLocalEventServer)
            {
                _wcfEventService = WcfEventService.EventService;
                try
                {
                    _wcfEventService.Start();
                }
                catch (Exception ex)
                {
                    Data.SendToErrorsLog("Ошибка при запуске локального сервера событий: " + ex.FullMessage());
                }
                // Подключаемся клиентом сами к себе для рассылки событий возможным клиентам
                Data.LocalEventClient = new EventClient.EventClient();
                Data.LocalEventClient.Connect("localhost", 9901, new[] {"Fetching", "Logging", "Trending"},
                                              PropertyUpdate, ShowError, ClientFileReceived, UpdateLocalConnectionStatus);
            }

            
            Data.SelfIpAddress = Data.Config.ReadString("DataEventServers", "SelfIpAddress", "");
            //var selfIpAddresses = Dns.GetHostAddresses(Dns.GetHostName());

            // Подключаемся клиентом к другим станциям
            for (var i = 1; i <= Data.StationNodes.Length; i++)
            {
                var enable = Data.Config.ReadBool("DataEventServers", "EnableEventServer" + i, false);
                Data.StationNodes[i - 1].Enable = enable;
                var addr = Data.Config.ReadString("DataEventServers", "AddressEventServer" + i, "");
                if (!enable) continue;
                IPAddress address;
                if (IPAddress.TryParse(addr, out address))
                {
                    Data.StationNodes[i - 1].Address = address;
                    if (addr.Equals(Data.SelfIpAddress))
                    {
                        Data.StationNodes[i - 1].ItThisStation = true;
                        Data.StationNodes[i - 1].Descriptor = "Эта станция";
                    }
                    if (!Data.StationNodes[i - 1].ItThisStation)
                    {
                        var id = Data.ConnectToEventServer(addr, 9901, new[] {"Fetching", "Logging", "Trending"},
                                                           PropertyUpdate, ShowError, ClientFileReceived,
                                                           UpdateConnectionStatus);
                        Data.StationNodes[i - 1].ClientId = id;
                    }
                }
                else
                    Data.SendToErrorsLog(
                        string.Format("Ошибка в IP-адресе для сервера подписки {0} в файле конфигурации", i));                
            }

            // ----
            lock (Data.RiserNodes)
            {
                foreach (var riser in Data.RiserNodes.Values)
                {
                    if (string.IsNullOrWhiteSpace(riser.Number)) continue;
                    var riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                  riser.Channel, riser.Overpass, riser.Way,
                                                  riser.Product, riser.Riser);
                    Data.UpdateRiserProperty(riserName, "Number", riser.Number);
                    Data.UpdateRiserProperty(riserName, "Ntype", riser.Ntype);
                    Data.UpdateRiserProperty(riserName, "FactHeight", riser.FactHeight.ToString("0"));
                    Data.UpdateRiserProperty(riserName, "Setpoint", riser.Setpoint.ToString("0"));
                    Data.UpdateRiserProperty(riserName, "FilledKind", "");
                }
            }
            // Запуск таймера для фонового удаления старых архивов
            _timer.Enabled = true;
        }

        private void PropertyUpdate(DateTime servertime, string category, string pointname, string propname,
                                    string value)
        {
            switch (category)
            {
                case "Logging":
                    Data.RemoteClientAppendToLog(pointname, propname, value);
                    break;
                case "Trending":
                    Data.RemoteClientAppendToTrend(pointname, propname, value);
                    break;
                case "Fetching":
                    Data.RemoteClientUpdatedRiserNode(pointname, propname, value);
                    break;
                default:
                    var mess = string.Format("{0}{1}{2}{3}", category, pointname, propname, value);
                    foreach (var panel in _panels.Where(panel => panel != null))
                        panel.StatusMessage = mess;
                    break;
            }

        }

        private void ShowError(string errormessage)
        {
            Data.SendToErrorsLog(errormessage);
            foreach (var panel in _panels.Where(panel => panel != null))
                panel.ErrorMessage = errormessage;
        }

        private static void UpdateLocalConnectionStatus(Guid clientId, string ipaddr, ClientConnectionStatus status)
        {
            lock (Data.StationNodes)
            {
                foreach (var stationNode in Data.StationNodes)
                {
                    if (stationNode.Enable && stationNode.ItThisStation)
                    {
                        stationNode.Actived = true;
                        stationNode.ConnectionStatus = status;
                    }
                    else
                        stationNode.Actived = false;
                }
            }
        }

        private static void UpdateConnectionStatus(Guid clientId, string ipaddr, ClientConnectionStatus status)
        {
            lock (Data.StationNodes)
            {
                foreach (var stationNode in Data.StationNodes)
                {
                    if (stationNode.Enable && !stationNode.ItThisStation && stationNode.ClientId == clientId)
                    {
                        stationNode.Actived = true;
                        stationNode.ConnectionStatus = status;
                    }
                    else
                        stationNode.Actived = false;
                }
            }
        }

        private static void ClientFileReceived(string tarfilename, int percent, bool complete)
        {
            //
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.SendToSystemLog(string.Format("Останов системы на станции {0}", Environment.MachineName));
            var dt = DateTime.Now + new TimeSpan(0, 0, 0, 1);
            while (dt > DateTime.Now) Application.DoEvents();
            //Код, выполняемый при остановке процесса
            try
            {
                if (_wcfEventService != null) _wcfEventService.Stop();
            }
            catch (Exception ex)
            {
                Data.SendToErrorsLog("Ошибка при остановке локального сервера подписки: " + ex.FullMessage());
            }

            foreach (var worker in Workers.Where(worker => worker.IsBusy))
            {
                worker.CancelAsync();
            }
            dt = DateTime.Now + new TimeSpan(0, 0, 0, 1);
            while (dt > DateTime.Now) Application.DoEvents();
            _fileLogsWatcher.Changed -= fileSystemLogWatcher_Changed;
            _fileLogsWatcher.Created -= fileSystemLogWatcher_Changed;
            _fileLogsWatcher.Dispose();
            if (Data.RunningAsShell() || Data.MustWinLogOff) WinLogOff();
        }

        public void WinLogOff()
        {
            OperatingWin32(0);
        }

        private static void OperatingWin32(int mode)
        {
            using (var mcWin32 = new ManagementClass("Win32_OperatingSystem"))
            {
                mcWin32.Get();
                // без прав ничего не выйдет
                mcWin32.Scope.Options.EnablePrivileges = true;
                var mboShutdownParams =
                    mcWin32.GetMethodParameters("Win32Shutdown");
                // 0 - завершение сеанса
                // 1 - завершение работы системы
                // 2 - перезагрузка
                // 4 - принудительное завершение сеанса
                // 5 - принудительное завершение работы системы
                // 6 - принудительная перезагрузка
                // 8 - выключение питания
                // 12 - принудительное выключение питания
                mboShutdownParams["Flags"] = mode.ToString(CultureInfo.InvariantCulture);
                mboShutdownParams["Reserved"] = "0";
                foreach (var manObj in mcWin32.GetInstances().Cast<ManagementObject>().
                    Where(manObj => manObj != null))
                {
                    manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
                }
            }
        }

        private static void LoadFetchConfiguration()
        {
            var list = new List<string>();
            const string section = "FetchConfiguration";
            if (Data.Config.SectionExists(section))
            {
                list.AddRange(Data.Config.ReadSectionValues(section));
            }
            else
            {
                list.Add("1;2;2;D;Дизель;33;63;2;MOXA_MB01;10.9.4.51;0;1,9600,N");
                list.Add("1;2;2;D;Дизель;65;95;2;MOXA_MB02;10.9.4.52;0;2,9600,N");
                list.Add("1;4;4;D;Дизель;1;32;1;MOXA_MB03;10.9.4.53;0;3,38400,N");
                list.Add("1;2;2;T;ТС;34;94;2;MOXA_MB04;10.9.4.54;0;1,38400,N");
                list.Add("2;35;3,5;D;Дизель;53;67;1;MOXA_MB05;10.9.4.55;-52;1,9600,N");
                list.Add("2;35;3,5;D;Дизель;68;104;1;MOXA_MB06;10.9.4.56;-52;2,19200,N");
                list.Add("2;35;3,5;M;Мазут, газойль;1;13;1;MOXA_MB07;10.9.4.57;100;1,38400,N");
                list.Add("2;35;3,5;M;Мазут, газойль;40;52;1;MOXA_MB07;10.9.4.57;100;1,38400,N");
                list.Add("2;35;3,5;M;Мазут, газойль;14;39;1;MOXA_MB08;10.9.4.58;100;2,38400,N");
                list.Add("4;12;12;B;Бензин;1;37;1;MOXA_MB09;10.9.4.59;0;1,19200,N");
                list.Add("4;13;13;D;Дизель;38;74;1;MOXA_MB10;10.9.4.60;0;1,19200,N");
                // для ремонтного канала
                list.Add("0;0;0;R;Для ремонта стояков;247;247;1;Для ремонта;10.9.4.61;0;1,9600,N");
                var n = 0;
                foreach (var line in list)
                {
                    Data.Config.WriteString(section, n.ToString("0"), line);
                    n++;
                }
                Data.Config.UpdateFile();
            }
            foreach (var line in list)
                AddFetchLine(line);
            // для ремонтных контроллеров
            foreach (var channelNode in Data.ChannelNodes.Where(item => item.Overpass > 0))
            {
                var channelName = String.Format("Channel{0}", channelNode.Index + 1);
                var addr = new RiserAddress
                {
                    Channel = channelNode.Index,
                    Overpass = channelNode.Overpass,
                    Way = channelNode.Way,
                    Product = channelNode.Product,
                    Riser = 247
                };
                var name = String.Format("N{0}{1}{2}{3:D3}", addr.Overpass, addr.Way, addr.Product, addr.Riser);
                var riserNode = CreateRiserNode(channelName, name, channelNode.Risers.Count, 0, channelNode, addr);
                channelNode.Risers.Add(riserNode);
                Data.RiserNodes.Add(addr, riserNode);
            }
        }

        private static void AddFetchLine(string config)
        {
            var vals = config.Split(new[] {';'});
            if (vals.Length != 12) return;
            var comvals = vals[11].Split(new[] {','});
            if (comvals.Length != 3) return;
            int overpass, way, first, last, step, offset, comport, baudrate;
            var parity = comvals[2];
            if (!int.TryParse(vals[0], out overpass) || !int.TryParse(vals[1], out way) ||
                !int.TryParse(vals[5], out first) || !int.TryParse(vals[6], out last) ||
                !int.TryParse(vals[7], out step) || !int.TryParse(vals[10], out offset) ||
                !int.TryParse(comvals[0], out comport) || !int.TryParse(comvals[1], out baudrate) ||
                !(new [] { "N","O","E"}).Contains(parity)) return;
            var arg = new FetchArg
                {
                    Channel = 0,
                    Overpass = overpass,
                    Way = way,
                    WayFine = vals[2],
                    Product = vals[3],
                    ProductFine = vals[4],
                    RiserFirst = first,
                    RiserLast = last,
                    RiserStep = step,
                    MoxaName = vals[8],
                    MoxaIp = vals[9],
                    NodeOffset = offset,
                    Comport = comport,
                    Baudrate = baudrate,
                    Parity = parity
                };

            var list = new List<RiserNode>();
            var channelNode = Data.ChannelNodes.FirstOrDefault(
                item => item.Name.Equals(arg.MoxaName) && item.IpAddr.Equals(arg.MoxaIp));
            var newChannel = false;

            const int fetchTime = 1;

            if (channelNode == null)
            {
                var risersList = new List<int>();
                for (var i = arg.RiserFirst; i <= arg.RiserLast; i = i + arg.RiserStep)
                    risersList.Add(i);
                risersList.Sort();
                var index = Data.ChannelNodes.Count;
                var channelName = String.Format("Channel{0}", index + 1);
                channelNode = new ChannelNode
                    {
                        Active = Data.Config.ReadBool("FetchChannels", channelName, true),
                        Index = index,
                        Name = arg.MoxaName,
                        IpAddr = arg.MoxaIp,

                        LinkType = Data.Config.ReadInteger("FetchChannels", channelName + "_LinkType", 0),
                        Comport = arg.Comport,
                        Baudrate = arg.Baudrate,
                        Parity = arg.Parity,

                        FetchTime = fetchTime,
                        NextFetching = DateTime.Now + new TimeSpan(fetchTime*TimeSpan.TicksPerSecond),
                        TotalRequests = 0,
                        TotalErrors = 0,
                        BarometerValue = 30,
                        MarginalLimit = 15,
                        FailLimit = 30,
                        TimeMarginal = new TimeSpan(0, 0, 15),
                        TimeFail = new TimeSpan(0, 1, 0),
                        SendTimeout = 3,
                        ReceiveTimeout = 3,
                        Risers = new List<RiserNode>(),
                        Overpass = arg.Overpass,
                        Way = arg.Way,
                        WayFine = arg.WayFine,
                        Product = arg.Product,
                        ProductFine = arg.ProductFine,
                        RisersRange = risersList.ToArray(),
                        RisersRangeFine = arg.RiserStep > 1
                                              ? String.Format("{0},{1} ... {2}", arg.RiserFirst,
                                                              arg.RiserFirst + 2, arg.RiserLast)
                                              : String.Format("{0} ... {1}", arg.RiserFirst, arg.RiserLast)
                    };
                channelNode.Descriptor = arg.Overpass == 0
                                             ? channelNode.ProductFine
                                             : String.Format("Эстакада {0}. Путь {1}. {2}. Стояки {3}",
                                                             channelNode.Overpass, channelNode.WayFine,
                                                             channelNode.ProductFine, channelNode.RisersRangeFine);
                newChannel = true;
            }
            arg.Channel = channelNode.Index;
            var npp = channelNode.Risers.Count;
            for (var riserNumber = arg.RiserFirst;
                 riserNumber <= arg.RiserLast;
                 riserNumber = riserNumber + arg.RiserStep)
            {
                var addr = new RiserAddress
                    {
                        Channel = channelNode.Index,
                        Overpass = arg.Overpass,
                        Way = arg.Way,
                        Product = arg.Product,
                        Riser = riserNumber
                    };
                var channelName = String.Format("Channel{0}", channelNode.Index + 1);
                var name = String.Format("N{0}{1}{2}{3:D3}", arg.Overpass, arg.Way, arg.Product, riserNumber);
                var riser = CreateRiserNode(channelName, name, npp, arg.NodeOffset, channelNode, addr);
                list.Add(riser);
                Data.RiserNodes.Add(addr, riser);
                // загрузка записанных заданий налива
                var riserKey = String.Format("N{0}{1}{2}{3}",
                                             addr.Overpass, addr.Way,
                                             addr.Product, addr.Riser.ToString("000"));
                riser.Number = Data.Tasks.ReadString(riserKey, "Number", "");
                riser.Ntype = Data.Tasks.ReadString(riserKey, "NType", "");
                riser.FactHeight = Data.Tasks.ReadInteger(riserKey, "FactHeight", 0);
                riser.Setpoint = Data.Tasks.ReadInteger(riserKey, "Setpoint", 0);
                npp++;
            }

            channelNode.Risers.AddRange(list);
            if (newChannel)
                Data.ChannelNodes.Add(channelNode);
            else
            {
                var risersList = new List<int>(channelNode.RisersRange);
                for (var i = arg.RiserFirst; i <= arg.RiserLast; i = i + arg.RiserStep)
                    risersList.Add(i);
                risersList.Sort();
                channelNode.RisersRange = risersList.ToArray();
                var addition = arg.RiserStep > 1
                                   ? String.Format(", {0},{1} ... {2}", arg.RiserFirst, arg.RiserFirst + 2,
                                                   arg.RiserLast)
                                   : String.Format(", {0} ... {1}", arg.RiserFirst, arg.RiserLast);
                channelNode.RisersRangeFine += addition;
                channelNode.Descriptor += addition;
            }

            var worker = new BackgroundWorker();
            Workers.Add(worker);
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync(arg);
        }

        private static RiserNode CreateRiserNode(string channelName, string name, int n, int nodeOffset,
                                                 ChannelNode channelNode, RiserAddress addr)
        {
            var riserNode = new RiserNode
                {
                    Active = Data.Config.ReadBool(channelName, name, true),
                    NodeType =  Data.Config.ReadInteger(channelName, name + "_NodeType", 0),
                    Index = n,
                    Name = name,
                    Node = addr.Riser + nodeOffset,
                    NextFetching = DateTime.Now + new TimeSpan(channelNode.FetchTime*TimeSpan.TicksPerSecond),
                    Channel = channelNode.Index,
                    Overpass = addr.Overpass,
                    Way = addr.Way,
                    Product = addr.Product,
                    Riser = addr.Riser,
                    TotalRequests = 0,
                    TotalErrors = 0,
                    BarometerValue = 30,
                    MarginalLimit = 15,
                    FailLimit = 30,
                    TimeMarginal = new TimeSpan(0, 0, 15),
                    TimeFail = new TimeSpan(0, 1, 0),
                };
            return riserNode;
        }

        private static void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var arg = (FetchArg) e.Argument;
            var lastsecond = DateTime.Now.Second;
            var lastminute = -1;
            var lasthour = -1;
            while (!worker.CancellationPending)
            {
                Thread.Sleep(400); // разрузка процессоров
                try
                {
                    var dt = DateTime.Now;
                    if (lastsecond == dt.Second) continue;
                    lastsecond = dt.Second;
                    Data.FetchRisers(worker, arg);
                    if (lastminute == dt.Minute) continue;
                    lastminute = dt.Minute;
                    if (lasthour == dt.Hour || dt.Minute != 0) continue;
                    lasthour = dt.Hour;
                }
                catch (Exception ex)
                {
                    var mess = ex.FullMessage();
                    Console.WriteLine(mess);
                    Data.SendToErrorsLog("Ошибка в цикле опроса канала: " + mess);
                }
            }
        }

        private static void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        private static Process RunningInstance()
        {
            var current = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(current.ProcessName);
            // Просматриваем все процессы
            return processes.Where(process => process.Id != current.Id).
                FirstOrDefault(process => Assembly.GetExecutingAssembly().
                    Location.Replace("/", "\\") == current.MainModule.FileName);
            // нет, таких процессов не найдено
        }

        public void LoginUser(string fullname, string shortname, UserLevel level)
        {
            if (Data.UserName != null)
                Data.SendToSystemLog(string.Format("Выход из системы {0} на станции {1}", Data.UserName, Environment.MachineName));
            Data.UserName = shortname;
            Data.UserLevel = level;
            Data.UserFullname = fullname;
            Data.UserLogged = true;
            Data.SendToSystemLog(string.Format("Вход в систему {0} на станции {1}", shortname, Environment.MachineName));

        }

        public void ResetLogin()
        {
            if (Data.UserName != null)
                Data.SendToSystemLog(string.Format("Выход из системы {0} на станции {1}", Data.UserName, Environment.MachineName));
            Data.UserName = null;
            Data.UserLevel = UserLevel.None;
            Data.UserFullname = null;
            Data.UserLogged = false;
            UserListKeeper.CurrentUser = null;
        }

        public bool UserLogged()
        {
            return Data.UserLogged;
        }

        public string CurrentUserFullname()
        {
            return Data.UserFullname;
        }

        private static void fileSystemLogWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name.StartsWith("sys"))
                Data.SystemLogCreatedOrChanged();
            else if (e.Name.StartsWith("switch"))
                Data.SwitchLogCreatedOrChanged();
            else if (e.Name.StartsWith("chan"))
                Data.ChangeLogCreatedOrChanged();
            else if (e.Name.StartsWith("fill"))
                Data.FillingLogCreatedOrChanged();
        }
    }
}
