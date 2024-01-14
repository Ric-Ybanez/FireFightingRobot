namespace FireFightingRobot.Models
{
    public class AddDeviceHistoryVm
    {
        public string DeviceKey { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Smoke { get; set; }
    }
}
