using Computer = LibreHardwareMonitor.Hardware.Computer;
using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;

namespace HardwareMonitor.Services;

public record TemperatureReading(string HardwareName, string SensorName, float Value, float? Max);

public class HardwareMonitorService
{
    private readonly Computer _computer;
    private bool _isRunning;

    public event Action<List<TemperatureReading>>? OnTemperaturesUpdated;
    public event Action<Exception>? OnError;

    public HardwareMonitorService()
    {
        _computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMotherboardEnabled = false,
            IsMemoryEnabled = false,
            IsStorageEnabled = false,
            IsNetworkEnabled = false
        };
    }

    public void Start()
    {
        try
        {
            _computer.Open();
            _isRunning = true;
        }
        catch (Exception ex)
        {
            OnError?.Invoke(new Exception("Не удалось открыть мониторинг железа. Попробуйте запустить от администратора.", ex));
        }
    }

    public void Update()
    {
        if (!_isRunning) return;

        try
        {
            var readings = new List<TemperatureReading>();
            float? maxTemp = null;

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();

                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        var reading = new TemperatureReading(
                            hardware.Name,
                            sensor.Name,
                            sensor.Value.Value,
                            sensor.Max);

                        readings.Add(reading);

                        if (sensor.Value > (maxTemp ?? float.MinValue))
                            maxTemp = sensor.Value;
                    }
                }
            }

            OnTemperaturesUpdated?.Invoke(readings);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _computer.Close();
    }
}