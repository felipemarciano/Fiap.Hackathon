using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using FFMpegCore;
using System.Drawing;
using System.IO.Compression;

namespace WorkerService
{
    public sealed class Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger) : BackgroundService
    {
        private const string ClassName = nameof(Worker);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                "{Name} is running.", ClassName);

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("{Name} is working.", ClassName);

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var requestProcessingService =
                    scope.ServiceProvider.GetRequiredService<IRequestProcessingService>();

                var blobStorageService =
                    scope.ServiceProvider.GetRequiredService<IBlobStorageService>();

                var listRequestProcessingService = await requestProcessingService.GetbyStatus(EStatusRequestProcessing.NotProcessed);

                foreach (var item in listRequestProcessingService)
                {
                    string downloadFilePath = Path.Combine(Path.GetTempPath(), item.RequestFilePath);
                    await blobStorageService.DownloadFileFromBlobAsync(item.RequestFilePath, downloadFilePath, stoppingToken);

                    var outputFolder = Path.Combine(Path.GetTempPath(), "\videos");

                    Directory.CreateDirectory(outputFolder);

                    var videoInfo = FFProbe.Analyse(downloadFilePath);

                    var duration = videoInfo.Duration;

                    var interval = TimeSpan.FromSeconds(20);

                    for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
                    {
                        Console.WriteLine($"Processing frame at: {currentTime}");

                        var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                        FFMpeg.Snapshot(downloadFilePath, outputPath, new Size(1920, 1080), currentTime);
                    }

                    string destinationZipFilePath = Path.Combine(Path.GetTempPath(), $"{item.Id}.zip");

                    ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);

                    await blobStorageService.UploadFileToBlobAsync(destinationZipFilePath, stoppingToken);

                    Console.WriteLine("Processing completed.");


                    await Task.Delay(1000, stoppingToken);
                }
            }
        }


        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                "{Name} is stopping.", ClassName);

            await base.StopAsync(stoppingToken);
        }
    }
}
