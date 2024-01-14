using FireFightingRobot.Domain.Interfaces;
using FireFightingRobot.Framework;

namespace FireFightingRobot.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        Result IUnitOfWork.Commit()
        {
            try
            {
                _dataContext.SaveChanges();

                return Result.OK();
            }

            catch (Exception ex)
            {
                return Result.Fail("Error attempting to save changes");
            }
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
