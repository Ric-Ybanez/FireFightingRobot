using FireFightingRobot.DAL;
using Microsoft.EntityFrameworkCore;

namespace FireFigthingRobot.ReadStack;

public class ReadContext : DataContext
{
    public ReadContext(DbContextOptions<DataContext> options)
           : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override int SaveChanges()
    {
        throw new NotImplementedException("Saving Data in ReadStack is not allowed");
    }
}