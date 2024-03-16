using ApplicationCore.Constants;

namespace Fiap.Hackaton.API.Models.Response
{
    public class UploadProcessResponse
    {
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartProcessingDate { get; set; }
        public DateTime EndProcessingDate { get; set; }
        public EStatusRequestProcessing Status { get; set; }
        public required string FilePath { get; set; }
    }
}
