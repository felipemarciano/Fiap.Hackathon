using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class RequestProcessingService : IRequestProcessingService
    {
        private readonly IRepository<RequestProcessing> _requestProcessingRepository;

        public RequestProcessingService(IRepository<RequestProcessing> requestProcessingRepository)
        {
            _requestProcessingRepository = requestProcessingRepository;
        }

        public Task CreateRequestProcessing(string requestFilePath)
        {
            throw new NotImplementedException();
        }

        public Task EndProcessing(string requestFilePath, string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<RequestProcessing> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task StartProcessing(string requestFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
