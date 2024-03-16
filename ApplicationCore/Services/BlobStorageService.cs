using ApplicationCore.Interfaces;
using Azure.Storage.Blobs;

namespace ApplicationCore.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public string Upload(byte[] base64Video)
        {
            var fileName = $"{Guid.NewGuid()}.mp4";

            var containerClient = _blobServiceClient.GetBlobContainerClient("videos");

            using var stream = new MemoryStream(base64Video);
            containerClient.UploadBlob(fileName, stream);

            return $"{_blobServiceClient.Uri.AbsoluteUri}/videos/{fileName}";
        }

        public async Task UploadFileToBlobAsync(string filePath, CancellationToken stoppingToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("videos");
            await blobContainerClient.CreateIfNotExistsAsync();

            string fileName = Path.GetFileName(filePath);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            Console.WriteLine($"Uploading to Blob storage as blob:\n\t {blobClient.Uri}");

            await blobClient.UploadAsync(filePath, true, stoppingToken);

            Console.WriteLine("Upload complete.");
        }

        public async Task DownloadFileFromBlobAsync(string blobName, string downloadFilePath, CancellationToken stoppingToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("videos");
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            Console.WriteLine($"Downloading blob to:\n\t {downloadFilePath}");

            await blobClient.DownloadToAsync(downloadFilePath, stoppingToken);

            Console.WriteLine("Download complete.");
        }
    }
}
