using FireFightingRobot.Framework.Interface;

namespace FireFightingRobot.DAL.Entities;

public partial class DeviceHistory : IDeviceInput
{
    public int DeviceHistoryId { get; set; }
    public int DeviceId { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Smoke { get; set; }
    public double HeatIndex { get; set; }
    public string FireDetected { get; set; }
    public int AlertLevel { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Device Device { get; set; }
}
