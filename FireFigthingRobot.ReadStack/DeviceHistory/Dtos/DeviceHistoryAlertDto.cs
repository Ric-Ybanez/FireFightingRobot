
namespace FireFigthingRobot.ReadStack.DeviceHistory.Dtos;

public class DeviceHistoryAlertDto
{
    public int DeviceId { get; set; }
    public string DeviceKey { get; set; }
    public string AlertLevel { get; set; } = "OK";
    public DateTime AsOfDate { get; set; }
}
