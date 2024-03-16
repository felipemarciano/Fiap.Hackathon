using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Azure.Storage.Blobs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddScoped<IRequestProcessingService, RequestProcessingService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(_ => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;

    var dbContext = scopedProvider.GetRequiredService<HackathonContext>();
    dbContext.Database.Migrate();
}

host.Run();
