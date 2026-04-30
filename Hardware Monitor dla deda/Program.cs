using HardwareMonitor;
using System;
using System.Windows.Forms;

namespace Hardware_Monitor_dla_deda
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool startMinimized = args.Length > 0 &&
                (args[0].ToLower() == "/minimized" || args[0].ToLower() == "/silent");

            Application.Run(new MainForm(startMinimized));
        }
    }
}