using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Azure.Storage.Blobs;
using FFMpegCore;
using System.Drawing;
using System.IO.Compression;
using static System.Net.WebRequestMethods;

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

                var blobServiceClient =
                        scope.ServiceProvider.GetRequiredService<BlobServiceClient>();

                var blobStorageService = new BlobStorageService(blobServiceClient);

                var listRequestProcessingService = await requestProcessingService.GetbyStatus(EStatusRequestProcessing.NotProcessed);

                foreach (var item in listRequestProcessingService)
                {
                    await requestProcessingService.StartProcessing(item.Id);

                    var outputFolder = Path.Combine(Path.GetTempPath(), @$"\images_{DateTime.Now.ToString("hh-mm-ss")}");
                    string downloadFilePath = Path.Combine(Path.GetTempPath(), item.Id.ToString());
                    await blobStorageService.DownloadFileFromBlobAsync(item.Id.ToString(), downloadFilePath, stoppingToken);

                    Directory.CreateDirectory(outputFolder);

                    var videoInfo = FFProbe.Analyse(downloadFilePath);

                    var duration = videoInfo.Duration;

                    var interval = TimeSpan.FromSeconds(20);

                    for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
                    {
                        Console.WriteLine($"Processing frame at: {currentTime}");

                        var outputPath = Path.Combine(outputFolder, $"frame_at_{item.Id}_{currentTime.TotalSeconds}.jpg");
                        FFMpeg.Snapshot(downloadFilePath, outputPath, new Size(1920, 1080), currentTime);
                    }

                    string fileName = $"{item.Id}_{DateTime.Now.ToString("hh-mm-ss")}.zip";
                    string destinationZipFilePath = Path.Combine(Path.GetTempPath(), fileName);

                    ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);

                    await blobStorageService.UploadFileToBlobAsync(destinationZipFilePath, stoppingToken);

                    await requestProcessingService.EndProcessing(item.Id, @$"https://dlsfiaphackathon.blob.core.windows.net/videos/{fileName}");

                    Console.WriteLine("Processing completed.");

                    await Task.Delay(1000, stoppingToken);

                    await DoWorkAsync(stoppingToken);
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
