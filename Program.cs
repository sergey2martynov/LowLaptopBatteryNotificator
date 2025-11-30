using System;
using System.Windows.Forms;

namespace BatteryNotification
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (!AutoStartManager.IsAutoStartEnabled())
            {
                AutoStartManager.EnableAutoStart();
            }
            
            Application.Run(new BatteryMonitorForm());
        }
    }
}

