using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddScoped<IRequestProcessingService, RequestProcessingService>();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    var dbContext = scopedProvider.GetRequiredService<HackathonContext>();
    dbContext.Database.Migrate();
}

host.Run();
