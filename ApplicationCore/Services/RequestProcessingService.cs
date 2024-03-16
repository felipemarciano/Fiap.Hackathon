using ApplicationCore.Constants;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;

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

        public async Task CreateRequestProcessing(byte[] base64Video)
        {
            var blobStorageUrl = _blobStorageService.Upload(base64Video);
            var requestProcess = new RequestProcessing(blobStorageUrl);

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
                Name = x.FilePath,
                FilePath = x.FilePath!,
                EndProcessingDate = x.DateEndProcessing,
                StartProcessingDate = x.DateStartProcessing,
                StartDate = x.DateCreate,
                Status = x.Status
            });
        }

        public async Task<IEnumerable<RequestProcessing>> GetbyStatus(EStatusRequestProcessing eStatusRequestProcessing)
        {
            var spec = new RequestProcessingSpecification(eStatusRequestProcessing);
            return await _requestProcessingRepository.ListAsync(spec);
        }

        private async Task StartProcessing(RequestProcessing requestProcess)
        {
            requestProcess.StartProcessing();

            await _requestProcessingRepository.UpdateAsync(requestProcess);
        }
    }
}
