using System;
using System.Linq;
using System.Windows;

namespace SmartWork.Wpf
{
    public class Bootstrapper : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Window startupWindow = null;

            if (args.Contains("--demo"))
            {
                startupWindow = new DemoWindow();
            }

            if (startupWindow == null)
            {
                startupWindow = new MainWindow();
            }

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run(startupWindow);
        }
    }
}
