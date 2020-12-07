using System;
using System.ComponentModel;
using System.Linq;

namespace MultiFilling
{
    public interface IUserInfo
    {
        void LoginUser(string fullname, string shortname, UserLevel style);
        void ResetLogin();
        bool UserLogged();
        string CurrentUserFullname();
    }

    public enum UserLevel
    {
        [Description("Нет регистрации")]
        None = 0,
        [Description("Оператор")]
        Oper = 1,
        [Description("Приборист")]
        Inst = 2,
        [Description("Технолог")]
        Super = 3,
        [Description("Инженер АСУТП")]
        Eng = 4,
        [Description("Администратор")]
        Admin = 5
    }

    [Serializable]
    public class UserCategory
    {
        public UserLevel Style { get; set; }
        public string Label { get; set; }
        public UserCategory(UserLevel style, string label)
        {
            Style = style;
            Label = label;
        }
    }

    public static class UserInfo
    {
        public static UserLevel GetCurrentLevel(string slevel)
        {
            var levels = (UserLevel[])Enum.GetValues(typeof(UserLevel));
            return levels.FirstOrDefault(item => item.ToString().Equals(slevel));
        }

        public static int UserLevelToInt(UserLevel level)
        {
            var levels = (UserLevel[])Enum.GetValues(typeof(UserLevel));
            return levels.TakeWhile(item => item != level).Count();
        }

        public static UserLevel IntToUserLevel(int index)
        {
            var levels = (UserLevel[])Enum.GetValues(typeof(UserLevel));
            var level = UserLevel.None;
            var n = 0;
            foreach (var item in levels)
            {
                if (n == index) { level = item; break; }
                n++;
            }
            return level;
        }
    }
}
