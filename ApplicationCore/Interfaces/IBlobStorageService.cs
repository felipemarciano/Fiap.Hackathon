namespace ApplicationCore.Interfaces
{
    public interface IBlobStorageService
    {
        string Upload(Guid id, byte[] base64Video);
        Task UploadFileToBlobAsync(string filePath, CancellationToken stoppingToken);
        Task DownloadFileFromBlobAsync(string blobName, string downloadFilePath, CancellationToken stoppingToken);
    }
}
