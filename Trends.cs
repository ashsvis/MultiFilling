using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace MultiFilling
{
    public static partial class Data
    {
        // ReSharper disable InconsistentNaming
        public static void SendToTrend(string name, RiserAddress addr, int level, int low, int high,
            int levlow, int levhigh, int levbigoff, int setpoint, int curlevel, int curalarm, 
            bool hr3_3, bool hr3_4, bool hr1_14, bool hr4_9, bool hr3_2, bool hr1_13, bool hr4_13, bool hr4_8,
            bool hr3_1, bool hr1_12, bool hr4_12, bool hr1_8, bool hr3_14, bool hr3_15, bool hr3_6, bool hr3_10, bool hr3_11,
            string sdt = null)
        {
            var dt = DateTime.Now;
            if (sdt != null) DateTime.TryParse(sdt, out dt);
            var list = new[] 
            { 
                dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                addr.Channel.ToString("0"),
                addr.Overpass.ToString("0"),
                addr.Way.ToString("0"),
                addr.Product,
                addr.Riser.ToString("0").PadLeft(3),
                level.ToString("0"),
                low.ToString("0"), 
                high.ToString("0"),
                levlow.ToString("0"), 
                levhigh.ToString("0"), 
                levbigoff.ToString("0"),
                setpoint.ToString("0"),
                curlevel.ToString("0"),
                curalarm.ToString("0"),
                hr3_3 ? "1" : "0",
                hr3_4 ? "1" : "0",
                hr1_14 ? "1" : "0",
                hr4_9 ? "1" : "0",
                hr3_2 ? "1" : "0",
                hr1_13 ? "1" : "0",
                hr4_13 ? "1" : "0",
                hr4_8 ? "1" : "0",
                hr3_1 ? "1" : "0",
                hr1_12 ? "1" : "0",
                hr4_12 ? "1" : "0",
                hr1_8 ? "1" : "0",
                hr3_14 ? "1" : "0",
                hr3_15 ? "1" : "0",
                hr3_6 ? "1" : "0",
                hr3_10 ? "1" : "0",
                hr3_11 ? "1" : "0"
            };
            AppendToTrend(dt, name, String.Join("\t", list));
        }
        // ReSharper restore InconsistentNaming

        private static void AppendToTrend(DateTime dt, string fileprefix, string content)
        {
            if (!Directory.Exists(HistoryFolder)) return;
            var datename = dt.ToString("yyMMdd");
            var filename = Path.Combine(HistoryFolder, String.Format("{0}{1}.trd", fileprefix, datename));
            ThreadPool.QueueUserWorkItem(arg =>
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Trendlocker)
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
                                LocalEventClient.UpdateProperty("Trending", fileprefix, datename, content, true);
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

        public static void RemoteClientAppendToTrend(string fileprefix, string datename, string content)
        {
            if (!Directory.Exists(HistoryFolder)) return;
            var filename = Path.Combine(HistoryFolder, String.Format("{0}{1}.trd", fileprefix, datename));
            ThreadPool.QueueUserWorkItem(arg =>
            {
                var count = 10;
                while (count > 0)
                {
                    try
                    {
                        lock (Trendlocker)
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

        public static SortedList<DateTime, int[]> LoadTrend(string trendname, DateTime from, DateTime to)
        {
            var results = new SortedList<DateTime, int[]>();
            var dt = from;
            while (dt <= to)
            {
                var filename = Path.Combine(HistoryFolder, String.Format("{0}_{1}.{2}",
                                                                         trendname, dt.ToString("yyMMdd"), "trd"));
                if (File.Exists(filename))
                {
                    var count = 10;
                    while (count > 0)
                    {
                        try
                        {
                            lock (Trendlocker)
                            {
                                foreach (var line in File.ReadLines(filename))
                                {
                                    var avals = line.Split(new[] {'\t'});
                                    if (avals.Length != 32) continue;
                                    var datetime = avals[0];
                                    DateTime st;
                                    if (!DateTime.TryParse(datetime, out st)) continue;
                                    if (st < @from || st > to) continue;
                                    ////var channel = avals[1];
                                    ////var overpass = avals[2];
                                    ////var way = avals[3];
                                    ////var product = avals[4];
                                    ////var riser = avals[5];
                                    int level,
                                        low,
                                        high,
                                        levlow,
                                        levhigh,
                                        levbigoff,
                                        setpoint,
                                        curlevel,
                                        curalarm,
                                    // ReSharper disable InconsistentNaming
                                        hr3_3,
                                        hr3_4,
                                        hr1_14,
                                        hr4_9,
                                        hr3_2,
                                        hr1_13,
                                        hr4_13,
                                        hr4_8,
                                        hr3_1,
                                        hr1_12,
                                        hr4_12,
                                        hr1_8,
                                        hr3_14,
                                        hr3_15,
                                        hr3_6,
                                        hr3_10,
                                        hr3_11;
                                    // ReSharper restore InconsistentNaming
                                    if (int.TryParse(avals[6], out level) &&
                                        int.TryParse(avals[7], out low) &&
                                        int.TryParse(avals[8], out high) &&
                                        int.TryParse(avals[9], out levlow) &&
                                        int.TryParse(avals[10], out levhigh) &&
                                        int.TryParse(avals[11], out levbigoff) &&
                                        int.TryParse(avals[12], out setpoint) &&
                                        int.TryParse(avals[13], out curlevel) &&
                                        int.TryParse(avals[14], out curalarm) &&
                                        int.TryParse(avals[15], out hr3_3) &&
                                        int.TryParse(avals[16], out hr3_4) &&
                                        int.TryParse(avals[17], out hr1_14) &&
                                        int.TryParse(avals[18], out hr4_9) &&
                                        int.TryParse(avals[19], out hr3_2) &&
                                        int.TryParse(avals[20], out hr1_13) &&
                                        int.TryParse(avals[21], out hr4_13) &&
                                        int.TryParse(avals[22], out hr4_8) &&
                                        int.TryParse(avals[23], out hr3_1) &&
                                        int.TryParse(avals[24], out hr1_12) &&
                                        int.TryParse(avals[25], out hr4_12) &&
                                        int.TryParse(avals[26], out hr1_8) &&
                                        int.TryParse(avals[27], out hr3_14) &&
                                        int.TryParse(avals[28], out hr3_15) &&
                                        int.TryParse(avals[29], out hr3_6) &&
                                        int.TryParse(avals[30], out hr3_10) &&
                                        int.TryParse(avals[31], out hr3_11) &&
                                        !results.ContainsKey(st))
                                    {
                                        results.Add(st,
                                                    new[]
                                                        {
                                                            level, low, high, levlow, levhigh, levbigoff,
                                                            setpoint, curlevel, curalarm,
                                                            hr3_3, hr3_4, hr1_14, hr4_9, hr3_2, hr1_13,
                                                            hr4_13,hr4_8, hr3_1, hr1_12, hr4_12, hr1_8,
                                                            hr3_14, hr3_15, hr3_6, hr3_10, hr3_11
                                                        });
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
                }
                dt = dt.AddDays(1);
            }
            return results;
        }

        public static Dictionary<string, Tuple<string, SortedList<DateTime, bool>>>
            LoadSwitches(string trendname, DateTime from, DateTime to)
        {
            var results = new Dictionary<string, Tuple<string, SortedList<DateTime, bool>>>();
            var dt = from;
            while (dt <= to)
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
                                foreach (var line in File.ReadLines(filename))
                                {
                                    var avals = line.Split(new[] {'\t'});
                                    if (avals.Length != 11) continue;
                                    var datetime = avals[0];
                                    DateTime st;
                                    if (!DateTime.TryParse(datetime, out st)) continue;
                                    if (st < @from || st > to) continue;
                                    var risername = avals[6];
                                    if (trendname != risername) continue;
                                    var bitname = avals[7];
                                    ////var oldvalue = avals[8];
                                    var newvalue = avals[9];
                                    var desc = avals[10];
                                    SortedList<DateTime, bool> list;
                                    if (!results.ContainsKey(bitname))
                                    {
                                        list = new SortedList<DateTime, bool>();
                                        results.Add(bitname,
                                                    new Tuple<string, SortedList<DateTime, bool>>(desc, list));
                                    }
                                    else
                                        list = results[bitname].Item2;
                                    if (!list.ContainsKey(st))
                                        list.Add(st, newvalue == "1");
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
                dt = dt.AddDays(1);
            }
            return results;
        }
    }

}
