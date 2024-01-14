namespace FireFightingRobot.DAL.Entities;

public partial class Device
{
    public Device() 
    {
        DeviceHistories = new HashSet<DeviceHistory>();
    }

    public int DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DeviceKey { get; set; }

    public virtual ICollection<DeviceHistory> DeviceHistories { get; set; }
}
