namespace FireFigthingRobot.ReadStack.DeviceHistory.Dtos;

public class DeviceHistoryDto
{
    public int DeviceId { get; set; }
    public string DeviceKey { get; set; }
    public string DeviceName { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Smoke { get; set; }
    public double HeatIndex { get; set; }
    public DateTime CreatedDate { get; set; }
}
