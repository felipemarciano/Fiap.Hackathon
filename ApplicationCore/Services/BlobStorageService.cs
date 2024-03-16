using ApplicationCore.Interfaces;
using ApplicationCore.Settings;
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
    }
}
