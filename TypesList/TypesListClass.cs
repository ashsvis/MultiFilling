using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace MultiFilling.TypesList
{
    [Serializable]
    public class TypeData
    {
        // ReSharper disable MemberCanBePrivate.Global
        public string NType { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int Diameter { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int Throat { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int Deflevel { get; set; }
        // ReSharper restore MemberCanBePrivate.Global

        private TypeData()
        {
            NType = string.Empty;
            Diameter = 0;
            Throat = 0;
            Deflevel = 0;
        }

        public TypeData(string ntype, int diameter, int throat, int deflevel)
        {
            NType = ntype;
            Diameter = diameter;
            Throat = throat;
            Deflevel = deflevel;
        }

        public override string ToString()
        {
            return NType;
        }
    }

    public static class TypeDataKeeper
    {
        private static string _lastfilename;

        internal static List<TypeData> WaggonTypes = new List<TypeData>();

        public static void Clear()
        {
            WaggonTypes.Clear();
        }

        public static void Add(string ntype, int diameter, int throat, int deflevel)
        {
            if (WaggonTypes.Any(item => item.NType.Equals(ntype))) return;
            WaggonTypes.Add(new TypeData(ntype, diameter, throat, deflevel));
            SaveTypesData(_lastfilename);
        }

        public static void Edit(string ntype, int diameter, int throat, int deflevel)
        {
            if (!WaggonTypes.Any(item => item.NType.Equals(ntype))) return;
            var wagtype = WaggonTypes.Find(item => item.NType == ntype);
            wagtype.Diameter = diameter;
            wagtype.Throat = throat;
            wagtype.Deflevel = deflevel;
            SaveTypesData(_lastfilename);
        }

        public static void Delete(string ntype)
        {
            if (!WaggonTypes.Any(item => item.NType.Equals(ntype))) return;
            WaggonTypes.RemoveAll(item => item.NType.Equals(ntype));
            SaveTypesData(_lastfilename);
        }

        public static int FindIndex(string ntype)
        {
            var n = 0;
            foreach (var wagtype in GetWaggonTypeItems().OrderBy(item => int.Parse(item.NType)))
            {
                if (wagtype.NType.Equals(ntype))
                    return n;
                n++;
            }
            return -1;
        }

        public static string GetWaggonType(int index)
        {
            var n = 0;
            foreach (var wagtype in GetWaggonTypeItems().OrderBy(item => int.Parse(item.NType)))
            {
                if (n == index)
                    return wagtype.NType;
                n++;
            }
            return null;
        }

        public static IEnumerable<TypeData> GetWaggonTypeItems()
        {
            return WaggonTypes;
        }

        public static void LoadTypesData(string filename)
        {
            _lastfilename = filename;
            WaggonTypes.Clear();
            if (!File.Exists(filename)) return;
            var trycount = 10;
            while (true)
            {
                try
                {
                    var s = new XmlSerializer(typeof(List<TypeData>));
                    using (TextReader r = new StreamReader(filename))
                    {
                        WaggonTypes = (List<TypeData>)s.Deserialize(r);
                    }
                    break;
                }
                catch (Exception)
                {
                    trycount--;
                    if (trycount <= 0) throw;
                    Thread.Sleep(500);
                }
            }
        }

        public static void SaveTypesData(string filename)
        {
            _lastfilename = filename;
            var trycount = 10;
            while (true)
            {
                try
                {
                    var s = new XmlSerializer(typeof(List<TypeData>));
                    using (TextWriter w = new StreamWriter(filename))
                    {
                        s.Serialize(w, WaggonTypes);
                        w.Flush();
                    }
                    break;
                }
                catch (Exception)
                {
                    trycount--;
                    if (trycount <= 0) throw;
                    Thread.Sleep(500);
                }
            }
        }
    }
}
