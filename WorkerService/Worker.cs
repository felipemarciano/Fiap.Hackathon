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
            logger.LogInformation(
                "{Name} is working.", ClassName);

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                var requestProcessingService =
                    scope.ServiceProvider.GetRequiredService<IRequestProcessingService>();

                var videoPath = @"Marvel_DOTNET_CSHARP.mp4";

                var outputFolder = @"\Images\";

                Directory.CreateDirectory(outputFolder);

                FFOptions fFOptions = new FFOptions();

                var videoInfo = FFProbe.Analyse(videoPath, fFOptions);
                var duration = videoInfo.Duration;

                var interval = TimeSpan.FromSeconds(20);

                for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
                {
                    Console.WriteLine($"Processando frame: {currentTime}");

                    var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                    FFMpeg.Snapshot(videoPath, outputPath, new Size(1920, 1080), currentTime);
                }

                string destinationZipFilePath = @"\Images\images.zip";

                ZipFile.CreateFromDirectory(outputFolder, destinationZipFilePath);

                Console.WriteLine("Processo finalizado.");

                //await 
                await Task.Delay(1000, stoppingToken);

                await DoWorkAsync(stoppingToken);
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
