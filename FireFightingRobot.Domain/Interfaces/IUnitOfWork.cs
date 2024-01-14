using FireFightingRobot.Framework;

namespace FireFightingRobot.Domain.Interfaces;

public interface IUnitOfWork
{
    Result Commit();
}
