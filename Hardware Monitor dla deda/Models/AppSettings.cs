using HardwareMonitor.Models;
using System.Text.Json;

namespace Hardware_Monitor_dla_deda.Models;

public class AppSettings
{
  public List<AlertRule> AlertRules { get; set; } = new();
  public string? SoundFilePath { get; set; }
  public float Volume { get; set; } = 0.8f;
  public int PollingIntervalMs { get; set; } = 1000;
  public bool StartMinimized { get; set; }
  public bool AutoStartWithWindows { get; set; }

  // Хоткей
  public int HotkeyModifiers { get; set; } = 2;     // Ctrl
  public int HotkeyKey { get; set; } = (int)Keys.F8; // F8
  public string? HotkeyDisplayString { get; set; } = "Ctrl + F8";

  private static readonly string SettingsPath =
      Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

  public static AppSettings Load()
  {
    try
    {
      if (File.Exists(SettingsPath))
      {
        var json = File.ReadAllText(SettingsPath);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
      }
    }
    catch { }
    return new AppSettings();
  }

  public void Save()
  {
    try
    {
      var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(SettingsPath, json);
    }
    catch { }
  }
}