using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace MultiFilling.UserList
{
    [Serializable]
    public class UserData
    {
// ReSharper disable MemberCanBePrivate.Global
        public string LastName { get; set; }
// ReSharper restore MemberCanBePrivate.Global
// ReSharper disable MemberCanBePrivate.Global
        public string FirstName { get; set; }
// ReSharper restore MemberCanBePrivate.Global
// ReSharper disable MemberCanBePrivate.Global
        public string MiddleName { get; set; }
// ReSharper restore MemberCanBePrivate.Global
        public bool Female { get; set; }
// ReSharper disable MemberCanBePrivate.Global
        public int Category { get; set; }
// ReSharper restore MemberCanBePrivate.Global
        public string PasswordHash { get; set; }

        private UserData()
        {
            LastName = String.Empty;
            FirstName = String.Empty;
            MiddleName = String.Empty;
            Female = false;
            Category = 0;
            PasswordHash = String.Empty;
        }

        public UserData(string last, string first, string middle, int category)
        {
            LastName = last;
            FirstName = first;
            MiddleName = middle;
            Female = false;
            Category = category;
            PasswordHash = String.Empty;
        }

        public string GetFullName()
        {
            return String.Concat(LastName, " ", FirstName, " ", MiddleName);
        }

        public string GetShortName()
        {
            return String.Concat(LastName, " ", FirstName.Substring(0, 1), ".",
                MiddleName.Substring(0, 1), ".");
        }
    }

    public static class UserListKeeper
    {
        internal static readonly List<string> Categories = 
            new List<string> { "Операторы", "Прибористы", "Инженеры-технологи",
             "Инженеры АСУ ТП", "Администраторы"};
        internal static readonly List<string> Category = 
            new List<string> { "Оператор", "Приборист", "Инженер-технолог",
             "Инженер АСУ ТП", "Администратор"};
        internal static List<UserData> Users = new List<UserData>();

        internal static UserData CurrentUser;

        public static int GetCurrentUserLevel()
        {
            if (CurrentUser != null)
                return CurrentUser.Category + 1;
            return 0;
        }

        public static string GetCurrentUserName()
        {
            return CurrentUser != null ? CurrentUser.GetShortName() : String.Empty;
        }

        public static string GetCurrentUserFullName()
        {
            return CurrentUser != null ? CurrentUser.GetFullName() : String.Empty;
        }

        public static void ShowEditor(Form parent, string filename)
        {
            LoadUsersData(filename);
            using (var f = new FrmUserList(true))
            {
                f.ShowDialog(parent);
            }
            SaveUsersData(filename);
        }

        public static bool ShowSelector(Form parent, string filename)
        {
            LoadUsersData(filename);
            using (var f = new FrmUserList())
            {
                f.Text = @"Вход в систему";
                return f.ShowDialog(parent) != DialogResult.Cancel;
            }
        }

        private static void LoadUsersData(string filename)
        {
            Users.Clear();
            if (!File.Exists(filename)) return;
            var trycount = 10;
            while (true)
            {
                try
                {
                    var s = new XmlSerializer(typeof (List<UserData>));
                    using (TextReader r = new StreamReader(filename))
                    {
                        Users = (List<UserData>) s.Deserialize(r);
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

        private static void SaveUsersData(string filename)
        {
            var trycount = 10;
            while (true)
            {
                try
                {
                    var s = new XmlSerializer(typeof(List<UserData>));
                    using (TextWriter w = new StreamWriter(filename))
                    {
                        s.Serialize(w, Users);
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

        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        internal static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        internal static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetMd5Hash(input);
            // Create a StringComparer an compare the hashes.
            var comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }

    }
}
