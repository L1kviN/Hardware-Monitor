using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace HardwareMonitor.Services
{
    public static class StartupManager
    {
        private const string AppName = "Hardware Monitor dla deda";
        private const string RegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static bool IsStartupEnabled()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath, false))
                {
                    return key?.GetValue(AppName) != null;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void SetStartup(bool enable)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
                {
                    if (enable)
                    {
                        string exePath = $"\"{Application.ExecutablePath}\" /minimized";
                        key?.SetValue(AppName, exePath);
                    }
                    else
                    {
                        key?.DeleteValue(AppName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось изменить настройки автозагрузки: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}