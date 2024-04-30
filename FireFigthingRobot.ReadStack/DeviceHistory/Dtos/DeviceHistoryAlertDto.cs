
namespace FireFigthingRobot.ReadStack.DeviceHistory.Dtos;

public class DeviceHistoryAlertDto
{
    public int DeviceId { get; set; }
    public string DeviceKey { get; set; }
    public int AlertLevel { get; set; }
    public string FireDetected { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime AsOfDate { get; set; }
}
