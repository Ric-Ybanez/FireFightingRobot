using FireFightingRobot.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FireFightingRobot.DAL
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        internal virtual DbSet<Device> Devices { get; set; }
        internal virtual DbSet<DeviceHistory> DeviceHistories { get; set; }
    }
}