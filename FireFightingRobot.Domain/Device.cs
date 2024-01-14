using FireFightingRobot.Framework;
using System.Text;

namespace FireFightingRobot.Domain;

public class Device
{
    public int DeviceId { get; internal set; }
    public string DeviceName { get; internal set; }
    public string DeviceKey { get; internal set; }

    private Device() { }

    public static Result<Device> Create(string deviceName)
    {
        if (string.IsNullOrEmpty(deviceName))
            return Result.Fail<Device>("Device name is required");

        return Result.OK(new Device
        {
            DeviceName = deviceName,
            DeviceKey = GenerateToken(deviceName)
        });
    }

    private static string GenerateToken(string deviceName) 
    {
        byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        byte[] key = Guid.NewGuid().ToByteArray();
        byte[] name = Encoding.ASCII.GetBytes(deviceName);
        return Convert.ToBase64String(time.Concat(key).Concat(name).ToArray());
    }
}