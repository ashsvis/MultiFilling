using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiFilling.WaggonsList
{
    [Serializable]
    public class WaggonData
    {
        // ReSharper disable MemberCanBePrivate.Global
        public string Number { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public string Ntype { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int FactHeight { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int Setpoint { get; set; }
        // ReSharper restore MemberCanBePrivate.Global

        public WaggonData()
        {
            Number = string.Empty;
            Ntype = string.Empty;
            FactHeight = 0;
        }

        public WaggonData(string number, string ntype, int factlevel)
        {
            Number = number;
            Ntype = ntype;
            FactHeight = factlevel;
        }
    }

    public static class WaggonDataKeeper
    {
        private const string WaggonFile = "waggons.txt";

        public static int Count()
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return 0;
            var count = File.ReadLines(filename).Count();
            return count;
        }

        public static int Find(string number)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return -1;
            var n = 0;
            foreach (var existnumber in File.ReadLines(filename)
                .Select(line => line.Split(new[] {'\t'})[0]))
            {
                if (string.CompareOrdinal(number, existnumber) == 0)
                    return n;
                n++;
            }
            return -1;
        }

        public static IEnumerable<string> GetLineItems(int skipCount, int takeCount)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            return !File.Exists(filename)
                ? new List<string>()
                : File.ReadLines(filename).Skip(skipCount).Take(takeCount);
        }

        public static void Delete(string number)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return;
            var tmpfilename = Path.Combine(Data.ConfigFolder, "~waggons.txt");
            if (File.Exists(tmpfilename)) File.Delete(tmpfilename);
            using (var stream = new FileStream(tmpfilename, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var line in from line in File.ReadLines(filename) 
                                         let existnumber = line.Split(new[] {'\t'})[0] 
                                         where !existnumber.Equals(number) select line)
                    {
                        writer.WriteLine(line);
                        writer.Flush();
                    }
                }
            }
            if (File.Exists(tmpfilename))
                File.Replace(tmpfilename, filename, Path.ChangeExtension(filename, ".bak"));
        }

        public static bool Edit(string number, string ntype, int factlevel)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return false;
            var tmpfilename = Path.Combine(Data.ConfigFolder, "~waggons.txt");
            if (File.Exists(tmpfilename)) File.Delete(tmpfilename);
            var modified = false;
            using (var stream = new FileStream(tmpfilename, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var line in File.ReadLines(filename))
                    {
                        var existnumber = line.Split(new[] {'\t'})[0];
                        if (string.CompareOrdinal(number, existnumber) == 0)
                        {
                            writer.WriteLine("{0}\t{1}\t{2}", existnumber, ntype, factlevel);
                            modified = true;
                        }
                        else
                            writer.WriteLine(line);
                        writer.Flush();
                    }
                }
            }
            if (File.Exists(tmpfilename))
                File.Replace(tmpfilename, filename, Path.ChangeExtension(filename, ".bak"));
            return modified;
        }

        public static bool Add(string number, string ntype, int factlevel)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return false;
            if (File.ReadLines(filename).Any(line => 
                line.Split(new[] {'\t'})[0].Equals(number))) return false;
            //--------------------------------------------------------------
            var tmpfilename = Path.Combine(Data.ConfigFolder, "~waggons.txt");
            if (File.Exists(tmpfilename)) File.Delete(tmpfilename);
            var lastnumber = "00000000";
            using (var stream = new FileStream(tmpfilename, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var added = false;
                    foreach (var line in File.ReadLines(filename))
                    {
                        var existnumber = line.Split(new[] {'\t'})[0];
                        if (string.CompareOrdinal(number, lastnumber) > 0 &&
                            string.CompareOrdinal(number, existnumber) < 0)
                        {
                            writer.WriteLine("{0}\t{1}\t{2}", number, ntype, factlevel);
                            writer.Flush();
                            added = true;
                        }
                        writer.WriteLine(line);
                        writer.Flush();
                        lastnumber = existnumber;
                    }
                    if (!added)
                    {
                        writer.WriteLine("{0}\t{1}\t{2}", number, ntype, factlevel);
                        writer.Flush();
                    }
                }
            }
            if (File.Exists(tmpfilename))
                File.Replace(tmpfilename, filename, Path.ChangeExtension(filename, ".bak"));
            return true;
        }

        public static WaggonData GetWaggon(string number)
        {
            var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
            if (!File.Exists(filename)) return null;
            foreach (var vals in from line in File.ReadLines(filename) 
                                 let existnumber = line.Split(new[] {'\t'})[0] 
                                 where string.CompareOrdinal(number, existnumber) == 0 
                                 select line.Split(new[] { '\t' }))
            {
                if (vals.Length != 3) return null;
                int factlev;
                return int.TryParse(vals[2], out factlev) 
                    ? new WaggonData(vals[0], vals[1], factlev) 
                    : null;
            }
            return null;
        }

       //public static IEnumerable<WaggonData> GetWaggonItems(int skipCount, int takeCount)
        //{
        //    var filename = Path.Combine(Data.ConfigFolder, WaggonFile);
        //    return !File.Exists(filename)
        //        ? new List<WaggonData>() 
        //        : File.ReadLines(filename).Skip(skipCount).Take(takeCount)
        //        .Select(GetWaggonItem);
        //}
    }
}
