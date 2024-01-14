using FireFightingRobot.Framework;

namespace FireFightingRobot.Domain.Interfaces;

public interface IDeviceRepository
{
    Result<Lazy<int>> Add(Device device);
    Result<Device> GetByName(string name);
    Result<Device> GetByKey(string key);
}
