using FireFightingRobot.Framework;
using Enums = FireFightingRobot.Framework.Enums;
using FireFightingRobot.Framework.Interface;

namespace FireFightingRobot.Domain;
public class DeviceHistory : IDeviceInput
{
    public int DeviceHistoryId { get; internal set; }
    public int DeviceId { get; internal set; }
    public double Temperature { get; internal set; }
    public double Humidity { get; internal set; }
    public double Smoke { get; internal set; }
    public double HeatIndex { get; internal set; }
    public string FireDetected { get; set; }
    public int AlertLevel { get; set; }
    public DateTime CreatedDate { get; internal set; }

    internal DeviceHistory() { }

    public static Result<DeviceHistory> Create(Device device, double temperature, double smoke, double humidity, string fireDetected)
    {

        var deviceHistory = new DeviceHistory
        {
            DeviceId = device.DeviceId,
            Temperature = temperature,
            Smoke = smoke,
            Humidity = humidity,
            HeatIndex = CalculateHeatIndex(temperature, humidity),
            FireDetected = fireDetected,
            CreatedDate = DateTime.Now
        };

        deviceHistory.AlertLevel = Enums.AlertLevel.FromInput(deviceHistory).Value;
        return Result.OK(deviceHistory);
    }

    private static double CalculateHeatIndex(double temperature, double humidity)
    {
        double temperatureFahrenheit = temperature * 9 / 5 + 32;
        double c1 = -42.379;
        double c2 = 2.04901523;
        double c3 = 10.14333127;
        double c4 = -0.22475541;
        double c5 = -6.83783e-03;
        double c6 = -5.481717e-02;
        double c7 = 1.22874e-03;
        double c8 = 8.5282e-04;
        double c9 = -1.99e-06;

        double temperatureSquare = Math.Pow(temperatureFahrenheit, 2);
        double humiditySquare = Math.Pow(humidity, 2);

        double heatIndex = c1 +
            c2 * temperatureFahrenheit +
            c3 * humidity +
            c4 * temperatureFahrenheit * humidity +
            c5 * temperatureSquare +
            c6 * humiditySquare +
            c7 * temperatureSquare * humidity +
            c8 * temperatureFahrenheit * humiditySquare +
            c9 * temperatureSquare * humiditySquare;

        return heatIndex;
    }

}