using Microsoft.EntityFrameworkCore;
using ScheduleControl.Entities.Models;

namespace ScheduleControl.DataAccess.Concrete.EntityFramework.Context
{
    public class ScheduleProjectDbContext : DbContext
    {
        public ScheduleProjectDbContext()
        {
        }

        public ScheduleProjectDbContext(DbContextOptions<ScheduleProjectDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AcerProDb;Trusted_Connection=true");
        }

        public DbSet<DevApp> DevApp { get; set; }
        public DbSet<User> User { get; set; }
    }
}