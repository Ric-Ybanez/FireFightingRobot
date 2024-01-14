using FireFightingRobot.Framework;

namespace FireFightingRobot.Domain.Interfaces;

public interface IDeviceHistoryRepository
{
    Result<Lazy<int>> Add(DeviceHistory device);
}
