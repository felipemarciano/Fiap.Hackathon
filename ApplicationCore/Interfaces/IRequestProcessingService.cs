using ApplicationCore.Constants;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IRequestProcessingService
    {
        Task CreateRequestProcessing(byte[] base64Video);
        Task EndProcessing(string requestFilePath, string filePath);
        Task<IEnumerable<UploadProcessReponseDTO>> GetAllUploadsAsync();
        Task<IEnumerable<RequestProcessing>> GetbyStatus(EStatusRequestProcessing eStatusRequestProcessing);
    }
}
