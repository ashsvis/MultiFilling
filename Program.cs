using System;
using System.Windows.Forms;

namespace MultiFilling
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += MyHandler;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception) args.ExceptionObject;
            Data.SendToErrorsLog("Не обслуживаемая ошибка: " + ex.FullMessage());
            if (args.IsTerminating)
                Data.SendToErrorsLog("Приложение будет аварийно завершено");
        }
    }
}
