using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Reflection.Metadata;

namespace ApplicationCore.Services
{
    public class RequestProcessingService : IRequestProcessingService
    {
        private readonly IRepository<RequestProcessing> _requestProcessingRepository;
        private readonly IBlobStorageService _blobStorageService;

        public RequestProcessingService(
            IRepository<RequestProcessing> requestProcessingRepository, 
            IBlobStorageService blobStorageService)
        {
            _requestProcessingRepository = requestProcessingRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task CreateRequestProcessing(string requestFilePath, byte[] base64Video)
        {
            var blobStorageUrl = _blobStorageService.Upload(base64Video);
            var requestProcess = new RequestProcessing(requestFilePath, blobStorageUrl);

            await _requestProcessingRepository.AddAsync(requestProcess);

            await StartProcessing(requestProcess);
        }

        public Task EndProcessing(string requestFilePath, string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UploadProcessReponseDTO>> GetAllUploadsAsync()
        {
            var uploadList =  await _requestProcessingRepository.ListAsync();

            return uploadList.Select(x => new UploadProcessReponseDTO
            {
                FilePath = x.FilePath!,
                Name = x.Name!,
                EndProcessingDate = x.DateEndProcessing,
                StartProcessingDate = x.DateStartProcessing,
                StartDate = x.DateCreate,
                Status = x.Status
            });

        }

        private async Task StartProcessing(RequestProcessing requestProcess)
        {
            requestProcess.StartProcessing();

            await _requestProcessingRepository.UpdateAsync(requestProcess);


        }
    }
}
