using ApplicationCore.Interfaces;
using ApplicationCore.Settings;
using Azure.Storage.Blobs;

namespace ApplicationCore.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly Configuration _settings;

        public BlobStorageService(BlobServiceClient blobServiceClient, Configuration settings)
        {
            _blobServiceClient = blobServiceClient;
            _settings = settings;
        }

        public string Upload(byte[] base64Video)
        {
            var fileName = $"{Guid.NewGuid()}.mp4";

            var containerClient = _blobServiceClient.GetBlobContainerClient(_settings.BlobStorageSettings.Container);

            using var stream = new MemoryStream(base64Video);
            containerClient.UploadBlob(fileName, stream);

            return $"{_blobServiceClient.Uri.AbsoluteUri}{_settings.BlobStorageSettings.Container}/{fileName}";
        }
    }
}
