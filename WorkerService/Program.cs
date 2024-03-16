using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    var dbContext = scopedProvider.GetRequiredService<HackathonContext>();
    dbContext.Database.Migrate();
}

host.Run();
