using ApplicationCore.Constants;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IRequestProcessingService
    {
        Task CreateRequestProcessing(byte[] base64Video);
        Task StartProcessing(Guid id);
        Task EndProcessing(Guid id, string filePath);
        Task<IEnumerable<UploadProcessReponseDTO>> GetAllUploadsAsync();
        Task<IEnumerable<RequestProcessing>> GetbyStatus(EStatusRequestProcessing eStatusRequestProcessing);
    }
}
