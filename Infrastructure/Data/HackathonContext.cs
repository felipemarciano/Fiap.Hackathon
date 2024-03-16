using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;

namespace Infrastructure.Data
{
    public class HackathonContext : DbContext
    {
        public HackathonContext(DbContextOptions<HackathonContext> options) : base(options) { }

        public DbSet<RequestProcessing> RequestProcessing { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
