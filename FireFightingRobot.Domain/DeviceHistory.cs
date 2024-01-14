using FireFightingRobot.Framework;

namespace FireFightingRobot.Domain;
public class DeviceHistory
{
    public int DeviceHistoryId { get; internal set; }
    public int DeviceId { get; internal set; }
    public double Temperature { get; internal set; }
    public double Humidity { get; internal set; }
    public double Smoke { get; internal set; }
    public double HeatIndex { get; internal set; }
    public DateTime CreatedDate { get; internal set; }

    internal DeviceHistory() { }

    public static Result<DeviceHistory> Create(Device device, double temperature, double smoke, double humidity)
    {
        return Result.OK(new DeviceHistory
        {
            DeviceId = device.DeviceId,
            Temperature = temperature,
            Smoke = smoke,
            Humidity = humidity,
            HeatIndex = CalculateHeatIndex(temperature, smoke, humidity),
            CreatedDate = DateTime.Now,
        });
    }

    private static double CalculateHeatIndex(double temperature, double smoke, double humidity) =>
        temperature + smoke + humidity;
}