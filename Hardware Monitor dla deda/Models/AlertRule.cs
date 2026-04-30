namespace HardwareMonitor.Models;

public class AlertRule
{
    public string HardwareName { get; set; } = string.Empty;
    public string SensorName { get; set; } = string.Empty;
    public float Threshold { get; set; }
    public bool IsEnabled { get; set; } = true;
    public float Hysteresis { get; set; } = 3f; // гистерезис для предотвращения дребезга
    public int DelaySeconds { get; set; } = 2;  // задержка перед срабатыванием
}