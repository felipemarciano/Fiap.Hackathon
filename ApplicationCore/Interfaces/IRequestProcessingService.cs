using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces
{
    public interface IRequestProcessingService
    {
        Task CreateRequestProcessing(string requestFilePath, byte[] base64Video);
        Task EndProcessing(string requestFilePath, string filePath);
        Task<IEnumerable<UploadProcessReponseDTO>> GetAllUploadsAsync();
    }
}
