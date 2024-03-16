using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IRequestProcessingService
    {
        Task CreateRequestProcessing(string requestFilePath);
        Task StartProcessing(string requestFilePath);
        Task EndProcessing(string requestFilePath, string filePath);
        Task<RequestProcessing> GetAll();
    }
}
