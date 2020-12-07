using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MultiFilling
{
    public delegate void SayToStatus(string mess);

    public static partial class Data
    {
        private static readonly object Loglocker = new object();
        private static readonly object Trendlocker = new object();
        private static string _lastSystemMessage = String.Empty;
        private static string[] _filterEventsList;
        private static Tuple<DateTime, DateTime> _filterDateTimeRange;
        private static string[] _filterOverpassList;
        private static string[] _filterWayList;
        private static string[] _filterProductList;
        private static Tuple<int, int> _filterRiserRange;

        public static DateTime MinRangeDate()
        {
            return new DateTime(1753, 1, 1);
        }

        public static DateTime MaxRangeDate()
        {
            return new DateTime(9998, 12, 31);
        }

        public static string[] FilterOverpassList
        {
            get
            {
                if (_filterOverpassList == null)
                {
                    const string section = "FilterAddress";
                    var list = Config.ReadString(section, "Overpass", "").Split(new[] {','});
                    _filterOverpassList = list.ToArray();
                }
                return _filterOverpassList;
            }
            set { _filterOverpassList = value; }
        }

        public static string[] FilterWayList
        {
            get
            {
                if (_filterWayList == null)
                {
                    const string section = "FilterAddress";
                    var list = Config.ReadString(section, "Way", "").Split(new[] { ',' });
                    _filterWayList = list.ToArray();
                }
                return _filterWayList;
            }
            set { _filterWayList = value; }
        }

        public static string[] FilterProductList
        {
            get
            {
                if (_filterProductList == null)
                {
                    const string section = "FilterAddress";
                    var list = Config.ReadString(section, "Product", "").Split(new[] { ',' });
                    _filterProductList = list.ToArray();
                }
                return _filterProductList;
            }
            set { _filterProductList = value; }
        }

        public static Tuple<int, int> FilterRiserRange
        {
            get
            {
                if (_filterRiserRange == null)
                {
                    const string section = "FilterAddress";
                    _filterRiserRange = new Tuple<int, int>(
                        Config.ReadInteger(section, "FirstRiser", 1),
                        Config.ReadInteger(section, "LastRiser", 247));
                }
                return _filterRiserRange;
            }
            set { _filterRiserRange = value; }
        }

        public static void UpdateFilterAddress(string[] overpass, string[] way, string[] product, int first, int last)
        {
            _filterOverpassList = overpass;
            _filterWayList = way;
            _filterProductList = product;
            _filterRiserRange = new Tuple<int, int>(first, last);
            const string section = "FilterAddress";
            Config.WriteString(section, "Overpass", string.Join(",", overpass));
            Config.WriteString(section, "Way", string.Join(",", way));
            Config.WriteString(section, "Product", string.Join(",", product));
            Config.WriteInteger(section, "FirstRiser", first);
            Config.WriteInteger(section, "LastRiser", last);
            Config.UpdateFile();
        }

        public static string LogsFolder { get; set; }
        public static string FileToErrorsFullName { get; set; }

        public static string[] FilterEventsList
        {
            get
            {
                if (_filterEventsList == null)
                {
                    var list = new List<string>();
                    const string section = "FilterEvents";
                    if (Config.SectionExists(section))
                        list.AddRange(Config.ReadSectionValues(section));
                    _filterEventsList = list.ToArray();
                }
                return _filterEventsList;
            }
            set { _filterEventsList = value; }
        }

        public static void UpdateFilterEventsList(string[] list)
        {
            _filterEventsList = list;
            const string section = "FilterEvents";
            if (Config.SectionExists(section))
                Config.EraseSection(section);
            var n = 0;
            foreach (var s in list)
            {
                Config.WriteString(section, n.ToString("0"), s);
                n++;
            }
            Config.UpdateFile();
        }

        public static Tuple<DateTime, DateTime> FilterDateTimeRange
        {
            get
            {
                if (_filterDateTimeRange == null)
                {
                    const string section = "FilterDateTimeRange";
                    _filterDateTimeRange = new Tuple<DateTime, DateTime>(
                        Config.ReadDateTime(section, "First", MinRangeDate()),
                        Config.ReadDateTime(section, "Last", MaxRangeDate()));
                }
                return _filterDateTimeRange;
            }
            set { _filterDateTimeRange = value; }
        }

        public static void UpdateFilterDateTimeRange(DateTime first, DateTime last)
        {
            _filterDateTimeRange = new Tuple<DateTime, DateTime>(first, last);
            const string section = "FilterDateTimeRange";
            Config.WriteDateTime(section, "First", first);
            Config.WriteDateTime(section, "Last", last);
            Config.UpdateFile();
        }

        public static void ResetErrorsLog()
        {
            var filename = FileToErrorsFullName;           
            if (!File.Exists(filename)) return;
            ThreadPool.QueueUserWorkItem(arg =>
            {
                var count = 10;
                while (count > 0)
                {
                    try
                    {
                        lock (Loglocker)
                        {
                            File.Copy(filename, Path.ChangeExtension(filename, ".~err"), true);
                            File.Delete(filename);
                        }
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(50);
                        count--;
                    }
                }
            });
        }

        private static string _errcontent = "";

        public static void SendToErrorsLog(string content, string sdt = null)
        {
            if (_errcontent.Equals(content)) return;
            _errcontent = content;
            var dt = DateTime.Now;
            if (sdt != null) DateTime.TryParse(sdt, out dt);
            var filename = FileToErrorsFullName;
            var list = new[]
                {
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    content
                };
            ThreadPool.QueueUserWorkItem(arg =>
            {
                var count = 10;
                while (count > 0)
                {
                    try
                    {
                        lock (Loglocker)
                        {
                            using (var stream = new FileStream(filename, FileMode.Append))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.WriteLine(String.Join("\t", list));
                                    writer.Flush();
                                }
                            }
                        }
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(50);
                        count--;
                    }
                }
            });           
        }

        public static event SayToStatus SayToStatus;

        public static void SendToSwitchLog(string name, RiserAddress addr, string param, string oldvalue,
                                           string newvalue, string descriptor, string sdt = null)
        {
            var dt = DateTime.Now;
            if (sdt != null) DateTime.TryParse(sdt, out dt);
            var list = new[]
                {
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    addr.Channel > 0 ? addr.Channel.ToString("0") : "",
                    addr.Overpass > 0 ? addr.Overpass.ToString("0") : "",
                    addr.Way > 0 ? addr.Way.ToString("0") : "",
                    addr.Product,
                    addr.Riser > 0 ? addr.Riser.ToString("0").PadLeft(3) : "",
                    name,
                    param,
                    oldvalue,
                    newvalue,
                    descriptor
                };
            AppendToLog(dt, "switch", String.Join("\t", list));
        }

        public static void SendToChangeLog(string name, RiserAddress addr, string param, string oldvalue,
                                           string newvalue, string autor, string descriptor, string sdt = null)
        {
            var dt = DateTime.Now;
            if (sdt != null) DateTime.TryParse(sdt, out dt);
            var list = new[]
                {
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    addr.Channel > 0 ? addr.Channel.ToString("0") : "",
                    addr.Overpass > 0 ? addr.Overpass.ToString("0") : "",
                    addr.Way > 0 ? addr.Way.ToString("0") : "",
                    addr.Product,
                    addr.Riser > 0 ? addr.Riser.ToString("0").PadLeft(3) : "",
                    name,
                    param,
                    oldvalue,
                    newvalue,
                    autor,
                    descriptor
                };
            AppendToLog(dt, "chan", String.Join("\t", list));
        }

        public static string GetFineProductName(string productCode)
        {
            switch (productCode)
            {
                case "D":
                    return "ДТ";
                case "M":
                    return "Мазут";
                case "B":
                    return "Бензин";
                case "T":
                    return "ТС";
            }
            return "";
        }

        private static FormWorklogFilterEditor _formWorklogFilterEditor;

        public static void ShowWorklogFilterEditor(Control panel)
        {
            if (_formWorklogFilterEditor == null)
            {
                _formWorklogFilterEditor = new FormWorklogFilterEditor();
                var size = _formWorklogFilterEditor.Size;
                _formWorklogFilterEditor.Location = new Point(
                    (panel.Size.Width - size.Width)/2,
                    (panel.Size.Height - size.Height)/2);
                _formWorklogFilterEditor.Show(panel);
            }
            _formWorklogFilterEditor.Visible = true;
            _formWorklogFilterEditor.BringToFront();
        }

        public static void SendToSystemLog(string descriptor,
                                           RiserAddress? addr = null, RiserWaggonData? waggon = null, string sdt = null)
        {
            descriptor = descriptor.Replace('\n', ' ');
            var addr1 = addr ?? new RiserAddress();
            var waggon1 = waggon ?? new RiserWaggonData();
            var key = String.Concat(descriptor,
                                    addr1.Channel,
                                    addr1.Overpass,
                                    addr1.Way,
                                    addr1.Product,
                                    addr1.Riser);
            if (_lastSystemMessage.Equals(key)) return;
            _lastSystemMessage = key;
            var dt = DateTime.Now;
            if (sdt != null) DateTime.TryParse(sdt, out dt);
            var list = new []
                {
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    addr1.Channel > 0 ? addr1.Channel.ToString("0") : "",
                    addr1.Overpass > 0 ? addr1.Overpass.ToString("0") : "",
                    addr1.Way > 0 ? addr1.Way.ToString("0") : "",
                    addr1.Product,
                    addr1.Riser > 0 ? addr1.Riser.ToString("0").PadLeft(3) : "",
                    descriptor,
                    waggon1.Number,
                    waggon1.Ntype,
                    waggon1.FactHeight > 0 ? waggon1.FactHeight.ToString("0") : "",
                    waggon1.Setpoint > 0 ? waggon1.Setpoint.ToString("0") : "",
                    waggon1.FilledLevel > 0 ? waggon1.FilledLevel.ToString("0") : ""
                };
            AppendToLog(dt, "sys", String.Join("\t", list));
            // отправка сообщения в статусную строку
            if (SayToStatus != null) SayToStatus(descriptor);
        }

        public static void SendToFillingLog(DateTime started, RiserAddress addr, RiserWaggonData waggon, 
            DateTime ended, string username, string descriptor)
        {
            descriptor = descriptor.Replace('\n', ' ');
            var list = new[]
                {
                    started.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    ended.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    addr.Channel > 0 ? addr.Channel.ToString("0") : "",
                    addr.Overpass > 0 ? addr.Overpass.ToString("0") : "",
                    addr.Way > 0 ? addr.Way.ToString("0") : "",
                    addr.Product,
                    addr.Riser > 0 ? addr.Riser.ToString("0").PadLeft(3) : "",
                    waggon.Number,
                    waggon.Ntype,
                    waggon.Setpoint.ToString("0"),
                    waggon.FilledLevel.ToString("0"),
                    username,
                    descriptor
                };
            AppendToLog(ended, "fill", String.Join("\t", list));
        }

        private static void AppendToLog(DateTime dt, string fileprefix, string content)
        {
            if (!Directory.Exists(LogsFolder)) return;
            var datename = dt.ToString("yyMMdd");
            var filename = Path.Combine(LogsFolder, String.Format("{0}{1}.log", fileprefix, datename));
            ThreadPool.QueueUserWorkItem(arg =>
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                using (var stream = new FileStream(filename, FileMode.Append))
                                {
                                    using (var writer = new StreamWriter(stream))
                                    {
                                        writer.WriteLine(content);
                                        writer.Flush();
                                    }
                                }
                            }
                            if (LocalEventClient != null)
                            {
                                //if (content.IndexOf("Вход в систему", StringComparison.Ordinal) > 0 ||
                                //    content.IndexOf("Выход из системы", StringComparison.Ordinal) > 0 ||
                                //    content.IndexOf("Запуск системы", StringComparison.Ordinal) > 0 ||
                                //    content.IndexOf("Останов системы", StringComparison.Ordinal) > 0) break;
                                LocalEventClient.UpdateProperty("Logging", fileprefix, datename, content, true);
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                });
        }

        public static void RemoteClientAppendToLog(string fileprefix, string datename, string content)
        {
            if (!Directory.Exists(LogsFolder)) return;
            var filename = Path.Combine(LogsFolder, String.Format("{0}{1}.log", fileprefix, datename));
            ThreadPool.QueueUserWorkItem(arg =>
            {
                var count = 10;
                while (count > 0)
                {
                    try
                    {
                        lock (Loglocker)
                        {
                            using (var stream = new FileStream(filename, FileMode.Append))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.WriteLine(content);
                                    writer.Flush();
                                }
                            }
                        }
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(50);
                        count--;
                    }
                }
            });
        }

        private static IEnumerable<string> ReadFillLogLines(DateTime dateBefore, bool filtering)
        {
            if (!Directory.Exists(LogsFolder)) return new string[] { };
            var events = FilterEventsList;
            var fdt = FilterDateTimeRange;
            var firstdate = fdt.Item1.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var lastdate = fdt.Item2.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var overpasses = FilterOverpassList;
            var ways = FilterWayList;
            var products = FilterProductList;
            var frs = FilterRiserRange;
            var firstriser = frs.Item1.ToString("0").PadLeft(3);
            var lastriser = frs.Item2.ToString("0").PadLeft(3);
            var list = new List<string>();
            var dt = DateTime.Now;
            while (dt >= dateBefore)
            {
                var filename = Path.Combine(LogsFolder,
                                            String.Format("fill{0}.log", dt.ToString("yyMMdd")));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                if (!filtering)
                                    list.InsertRange(0, File.ReadLines(filename));
                                else
                                {
                                    var filteredlist = new List<string>();
                                    foreach (var line in File.ReadLines(filename))
                                    {
                                        var avals = line.Split(new[] { '\t' });
                                        if (avals.Length != 13) continue;
                                        ////var startdatetime = avals[0];
                                        var enddatetime = avals[1];
                                        ////var channel = avals[2];
                                        var overpass = avals[3];
                                        var way = avals[4];
                                        var product = avals[5];
                                        var riser = avals[6];
                                        ////var number = avals[7];
                                        ////var type = avals[8];
                                        ////var setpoint = avals[9];
                                        ////var filled = avals[10];
                                        ////var user = avals[11];
                                        var mess = avals[12];
                                        if (string.CompareOrdinal(enddatetime, firstdate) < 0 ||
                                            string.CompareOrdinal(enddatetime, lastdate) > 0) continue;
                                        if (!events.Any(mess.StartsWith)) continue;
                                        if (!string.IsNullOrWhiteSpace(overpass) && !overpasses.Contains(overpass))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(way) && !ways.Contains(way)) continue;
                                        if (!string.IsNullOrWhiteSpace(product) && !products.Contains(product))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(riser))
                                        {
                                            if (!(String.Compare(riser, firstriser, StringComparison.Ordinal) >= 0 &&
                                                  String.Compare(riser, lastriser, StringComparison.Ordinal) <= 0))
                                                continue;
                                        }
                                        filteredlist.Add(line);
                                    }
                                    list.InsertRange(0, filteredlist);
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
                dt = dt.AddDays(-1);
            }
            return list;
        }

        public static ArrayList GetFillLogRecords(int pos, int count, DateTime dateBefore,
            out int linescount, bool filltering)
        {
            var results = new ArrayList();
            linescount = 0;
            if (!Directory.Exists(LogsFolder)) return results;
            if (count > 0)
            {
                try
                {
                    var list = new List<string>();
                    linescount = ReadFillLogLines(dateBefore, filltering).Count();
                    list.AddRange(linescount <= count
                                      ? ReadFillLogLines(dateBefore, filltering)
                                      : ReadFillLogLines(dateBefore, filltering).Skip(linescount - count - pos).Take(count));
                    //----------------------------------------------------------------------------
                    var i = count;
                    foreach (var rec in list.Select(line => line.Split(new[] { '\t' })))
                    {
                        results.Add(rec);
                        if (--i == 0) break;
                    }
                }
                catch
                {
                    return new ArrayList();
                }
            }
            return results;
        }

        private static IEnumerable<string> ReadSysLogLines(DateTime dateBefore, bool filtering)
        {
            if (!Directory.Exists(LogsFolder)) return new string[] {};
            var events = FilterEventsList;
            var fdt = FilterDateTimeRange;
            var firstdate = fdt.Item1.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var lastdate = fdt.Item2.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var overpasses = FilterOverpassList;
            var ways = FilterWayList;
            var products = FilterProductList;
            var frs = FilterRiserRange;
            var firstriser = frs.Item1.ToString("0").PadLeft(3);
            var lastriser = frs.Item2.ToString("0").PadLeft(3);
            var list = new List<string>();
            var dt = DateTime.Now;
            while (dt >= dateBefore)
            {
                var filename = Path.Combine(LogsFolder,
                                            String.Format("sys{0}.log", dt.ToString("yyMMdd")));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                if (!filtering)
                                    list.InsertRange(0, File.ReadLines(filename));
                                else
                                {
                                    var filteredlist = new List<string>();
                                    foreach (var line in File.ReadLines(filename))
                                    {
                                        var avals = line.Split(new[] {'\t'});
                                        if (avals.Length != 12) continue;
                                        var datetime = avals[0];
                                        ////var channel = avals[1];
                                        var overpass = avals[2];
                                        var way = avals[3];
                                        var product = avals[4];
                                        var riser = avals[5];
                                        var mess = avals[6];
                                        ////var number = avals[7];
                                        ////var type = avals[8];
                                        ////var maxheight = avals[9];
                                        ////var setpoint = avals[10];
                                        ////var filled = avals[11];
                                        if (string.CompareOrdinal(datetime, firstdate) < 0 ||
                                            string.CompareOrdinal(datetime, lastdate) > 0) continue;
                                        if (!events.Any(mess.StartsWith)) continue;
                                        if (!string.IsNullOrWhiteSpace(overpass) && !overpasses.Contains(overpass))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(way) && !ways.Contains(way)) continue;
                                        if (!string.IsNullOrWhiteSpace(product) && !products.Contains(product))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(riser))
                                        {
                                            if (!(String.Compare(riser, firstriser, StringComparison.Ordinal) >= 0 &&
                                                  String.Compare(riser, lastriser, StringComparison.Ordinal) <= 0))
                                                continue;
                                        }
                                        filteredlist.Add(line);
                                    }
                                    list.InsertRange(0, filteredlist);
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
                dt = dt.AddDays(-1);
            }
            return list;
        }

        public static ArrayList GetSysLogRecords(int pos, int count, DateTime dateBefore, 
            out int linescount, bool filltering)
        {
            var results = new ArrayList();
            linescount = 0;
            if (!Directory.Exists(LogsFolder)) return results;
            if (count > 0)
            {
                try
                {
                    var list = new List<string>();
                    linescount = ReadSysLogLines(dateBefore, filltering).Count();
                    list.AddRange(linescount <= count
                                      ? ReadSysLogLines(dateBefore, filltering)
                                      : ReadSysLogLines(dateBefore, filltering).Skip(linescount - count - pos).Take(count));
                    //----------------------------------------------------------------------------
                    var i = count;
                    foreach (var rec in list.Select(line => line.Split(new[] { '\t' })))
                    {
                        if (rec.Length > 0)
                        {
                            DateTime t;
                            if (DateTime.TryParse(rec[0], out t))
                                rec[0] = String.Format("{0}", t.ToString("dd.MM.yy ddd HH:mm:ss.fff"));
                        }
                        results.Add(rec);
                        if (--i == 0) break;
                    }
                }
                catch
                {
                    return new ArrayList();
                }
            }
            return results;
        }

        private static IEnumerable<string> ReadSwitchLogLines(DateTime dateBefore, bool filtering)
        {
            if (!Directory.Exists(LogsFolder)) return new string[] { };
            var fdt = FilterDateTimeRange;
            var firstdate = fdt.Item1.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var lastdate = fdt.Item2.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var overpasses = FilterOverpassList;
            var ways = FilterWayList;
            var products = FilterProductList;
            var frs = FilterRiserRange;
            var firstriser = frs.Item1.ToString("0").PadLeft(3);
            var lastriser = frs.Item2.ToString("0").PadLeft(3);
            var list = new List<string>();
            var dt = DateTime.Now;
            while (dt >= dateBefore)
            {
                var filename = Path.Combine(LogsFolder,
                                            String.Format("switch{0}.log", dt.ToString("yyMMdd")));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                if (!filtering)
                                    list.InsertRange(0, File.ReadLines(filename));
                                else
                                {
                                    var filteredlist = new List<string>();
                                    foreach (var line in File.ReadLines(filename))
                                    {
                                        var avals = line.Split(new[] { '\t' });
                                        if (avals.Length != 11) continue;
                                        var datetime = avals[0];
                                        ////var channel = avals[1];
                                        var overpass = avals[2];
                                        var way = avals[3];
                                        var product = avals[4];
                                        var riser = avals[5];
                                        ////var name = avals[6];
                                        ////var param = avals[7];
                                        ////var oldstate = avals[8];
                                        ////var newstate = avals[9];
                                        ////var desc = avals[10];
                                        if (string.CompareOrdinal(datetime, firstdate) < 0 ||
                                            string.CompareOrdinal(datetime, lastdate) > 0) continue;
                                        if (!string.IsNullOrWhiteSpace(overpass) && !overpasses.Contains(overpass))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(way) && !ways.Contains(way)) continue;
                                        if (!string.IsNullOrWhiteSpace(product) && !products.Contains(product))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(riser))
                                        {
                                            if (!(String.Compare(riser, firstriser, StringComparison.Ordinal) >= 0 &&
                                                  String.Compare(riser, lastriser, StringComparison.Ordinal) <= 0))
                                                continue;
                                        }
                                        filteredlist.Add(line);
                                    }
                                    list.InsertRange(0, filteredlist);
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
                dt = dt.AddDays(-1);
            }
            return list;
        }

        public static ArrayList GetSwitchLogRecords(int pos, int count, DateTime dateBefore,
            out int linescount, bool filltering)
        {
            var results = new ArrayList();
            linescount = 0;
            if (!Directory.Exists(LogsFolder)) return results;
            if (count > 0)
            {
                try
                {
                    var list = new List<string>();
                    linescount = ReadSwitchLogLines(dateBefore, filltering).Count();
                    list.AddRange(linescount <= count
                                      ? ReadSwitchLogLines(dateBefore, filltering)
                                      : ReadSwitchLogLines(dateBefore, filltering).Skip(linescount - count - pos).Take(count));
                    //----------------------------------------------------------------------------
                    var i = count;
                    foreach (var rec in list.Select(line => line.Split(new[] { '\t' })))
                    {
                        if (rec.Length > 0)
                        {
                            DateTime t;
                            if (DateTime.TryParse(rec[0], out t))
                                rec[0] = String.Format("{0}", t.ToString("dd.MM.yy ddd HH:mm:ss.fff"));
                        }
                        results.Add(rec);
                        if (--i == 0) break;
                    }
                }
                catch
                {
                    return new ArrayList();
                }
            }
            return results;
        }

        private static IEnumerable<string> ReadSwitchLogLines(DateTime dateBefore, RiserAddress addr)
        {
            if (!Directory.Exists(LogsFolder)) return new string[] {};
            const string fileprefix = "switch";
            var list = new List<string>();
            var dt = DateTime.Now;
            while (dt >= dateBefore)
            {
                var filename = Path.Combine(LogsFolder,
                                            String.Format("{0}{1}.log", fileprefix, dt.ToString("yyMMdd")));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                var filteredlist = new List<string>();
                                var key = string.Concat(addr.Overpass, '\t', addr.Way, '\t', addr.Product, '\t',
                                                        addr.Riser.ToString("0").PadLeft(3));
                                filteredlist.AddRange(File.ReadLines(filename)
                                                          .Where(
                                                              line => line.IndexOf(key, StringComparison.Ordinal) >= 0));
                                list.InsertRange(0, filteredlist);
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
                dt = dt.AddDays(-1);
            }
            return list;
        }

        public static ArrayList GetSwitchLogRecords(int pos, int count, DateTime dateBefore,
            out int linescount, RiserAddress addr)
        {
            var results = new ArrayList();
            linescount = 0;
            if (!Directory.Exists(LogsFolder)) return results;
            if (count > 0)
            {
                try
                {
                    var list = new List<string>();
                    linescount = ReadSwitchLogLines(dateBefore, addr).Count();
                    list.AddRange(linescount <= count
                                      ? ReadSwitchLogLines(dateBefore, addr)
                                      : ReadSwitchLogLines(dateBefore, addr).Skip(linescount - count - pos).Take(count));
                    //----------------------------------------------------------------------------
                    var i = count;
                    foreach (var rec in list.Select(line => line.Split(new[] { '\t' })))
                    {
                        if (rec.Length > 0)
                        {
                            DateTime t;
                            if (DateTime.TryParse(rec[0], out t))
                                rec[0] = String.Format("{0}", t.ToString("dd.MM.yy ddd HH:mm:ss.fff"));
                        }
                        results.Add(rec);
                        if (--i == 0) break;
                    }
                }
                catch
                {
                    return new ArrayList();
                }
            }
            return results;
        }

        private static IEnumerable<string> ReadChangeLogLines(DateTime dateBefore, bool filtering)
        {
            if (!Directory.Exists(LogsFolder)) return new string[] { };
            var fdt = FilterDateTimeRange;
            var firstdate = fdt.Item1.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var lastdate = fdt.Item2.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var overpasses = FilterOverpassList;
            var ways = FilterWayList;
            var products = FilterProductList;
            var frs = FilterRiserRange;
            var firstriser = frs.Item1.ToString("0").PadLeft(3);
            var lastriser = frs.Item2.ToString("0").PadLeft(3);
            var list = new List<string>();
            var dt = DateTime.Now;
            while (dt >= dateBefore)
            {
                var filename = Path.Combine(LogsFolder,
                                            String.Format("chan{0}.log", dt.ToString("yyMMdd")));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Loglocker)
                            {
                                if (!filtering)
                                    list.InsertRange(0, File.ReadLines(filename));
                                else
                                {
                                    var filteredlist = new List<string>();
                                    foreach (var line in File.ReadLines(filename))
                                    {
                                        var avals = line.Split(new[] { '\t' });
                                        if (avals.Length != 12) continue;
                                        var datetime = avals[0];
                                        ////var channel = avals[1];
                                        var overpass = avals[2];
                                        var way = avals[3];
                                        var product = avals[4];
                                        var riser = avals[5];
                                        ////var name = avals[6];
                                        ////var param = avals[7];
                                        ////var oldstate = avals[8];
                                        ////var newstate = avals[9];
                                        ////var user = avals[10];
                                        ////var desc = avals[11];
                                        if (string.CompareOrdinal(datetime, firstdate) < 0 ||
                                            string.CompareOrdinal(datetime, lastdate) > 0) continue;
                                        if (!string.IsNullOrWhiteSpace(overpass) && !overpasses.Contains(overpass))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(way) && !ways.Contains(way)) continue;
                                        if (!string.IsNullOrWhiteSpace(product) && !products.Contains(product))
                                            continue;
                                        if (!string.IsNullOrWhiteSpace(riser))
                                        {
                                            if (!(String.Compare(riser, firstriser, StringComparison.Ordinal) >= 0 &&
                                                  String.Compare(riser, lastriser, StringComparison.Ordinal) <= 0))
                                                continue;
                                        }
                                        filteredlist.Add(line);
                                    }
                                    list.InsertRange(0, filteredlist);
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
                dt = dt.AddDays(-1);
            }
            return list;
        }

        public static ArrayList GetChangeLogRecords(int pos, int count, DateTime dateBefore,
            out int linescount, bool filltering)
        {
            var results = new ArrayList();
            linescount = 0;
            if (!Directory.Exists(LogsFolder)) return results;
            if (count > 0)
            {
                try
                {
                    var list = new List<string>();
                    linescount = ReadChangeLogLines(dateBefore, filltering).Count();
                    list.AddRange(linescount <= count
                                      ? ReadChangeLogLines(dateBefore, filltering)
                                      : ReadChangeLogLines(dateBefore, filltering).Skip(linescount - count - pos).Take(count));
                    //----------------------------------------------------------------------------
                    var i = count;
                    foreach (var rec in list.Select(line => line.Split(new[] { '\t' })))
                    {
                        if (rec.Length > 0)
                        {
                            DateTime t;
                            if (DateTime.TryParse(rec[0], out t))
                                rec[0] = String.Format("{0}", t.ToString("dd.MM.yy ddd HH:mm:ss.fff"));
                        }
                        results.Add(rec);
                        if (--i == 0) break;
                    }
                }
                catch
                {
                    return new ArrayList();
                }
            }
            return results;
        }

        public static void DeleteFromLogs(DateTime snaptime)
        {
            if (!Directory.Exists(LogsFolder)) return;
            var files = Directory.GetFiles(LogsFolder, "*.log");
            var key = snaptime.ToString("yyMMdd");
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName != null)
                {
                    var items = fileName.Split(
                        new[] { "switch", "chan", "sys", "fill", "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length != 2 || String.CompareOrdinal(items[0], key) >= 0) continue;
                }
                var count = 10;
                while (count > 0)
                {
                    try
                    {
                        File.Delete(file);
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(50);
                        count--;
                    }
                }
            }
        }

        public static void DeleteFromTrends(DateTime snaptime)
        {
            var path = HistoryFolder;
            if (!Directory.Exists(path)) return;
            var files = Directory.GetFiles(path, "*.trd");
            var key = snaptime.ToString("yyMMdd");
            lock (Trendlocker)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    if (fileName == null) continue;
                    var items = fileName.Split(new[] { '_', '.' });
                    if (items.Length != 3 || String.CompareOrdinal(items[1], key) >= 0) continue;
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            File.Delete(file);
                            break;
                        }
                        catch
                        {
                            Thread.Sleep(50);
                            count--;
                        }
                    }
                }
            }
        }

        public static event EventHandler OnUpdateSystemLog;
        public static event EventHandler OnUpdateSwitchLog;
        public static event EventHandler OnUpdateChangeLog;
        public static event EventHandler OnUpdateWorkLogFilter;
        public static event EventHandler OnUpdateFillingLog;

        public static void WorkLogFilterChanged()
        {
            if (OnUpdateWorkLogFilter != null) OnUpdateWorkLogFilter(null, null);
        }

        public static void FillingLogCreatedOrChanged()
        {
            if (OnUpdateFillingLog != null) OnUpdateFillingLog(null, null);
        }

        public static void SystemLogCreatedOrChanged()
        {
            if (OnUpdateSystemLog != null) OnUpdateSystemLog(null, null);
        }

        public static void SwitchLogCreatedOrChanged()
        {
            if (OnUpdateSwitchLog != null) OnUpdateSwitchLog(null, null);
        }

        public static void ChangeLogCreatedOrChanged()
        {
            if (OnUpdateChangeLog != null) OnUpdateChangeLog(null, null);
        }
    }
}
