using ApplicationCore.Interfaces;
using Fiap.Hackaton.API.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Hackaton.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;
        private readonly IRequestProcessingService _requestService;

        public UploadController(ILogger<UploadController> logger, IRequestProcessingService requestProcessingService)
        {
            _logger = logger;
            _requestService = requestProcessingService;
        }

        [AllowAnonymous]
        [HttpPost("/v1/upload:start", Name = "upload-process")]
        public async Task CreateProcessRequest(UploadProcessRequest request)
        {
            await _requestService.CreateRequestProcessing(request.Base64Video);
        }

        [AllowAnonymous]
        [HttpGet("/v1/uploads", Name = "list-uploads-history")]
        public async Task<IActionResult> GetUploadsHistories()
        {
            var result = await _requestService.GetAllUploadsAsync();

            return new OkObjectResult(result);
        }
    }
}
