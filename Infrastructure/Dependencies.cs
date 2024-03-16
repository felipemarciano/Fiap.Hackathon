
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string not found.");
            services.AddDbContext<HackathonContext>(c => c.UseSqlServer(connectionString));
        }
    }
}
