using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ymapmover
{
    static class Program
    {
        public static bool OpenDetailFormOnClose { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenDetailFormOnClose = false;

            Application.Run(new Startup());

            if (OpenDetailFormOnClose)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
