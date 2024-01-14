using FireFightingRobot.DAL;
using Microsoft.EntityFrameworkCore;

namespace FireFigthingRobot.ReadStack;

public class ReadContext : DataContext
{
    public ReadContext(DbContextOptions<DataContext> options)
       : base(options)
    {
    }
}