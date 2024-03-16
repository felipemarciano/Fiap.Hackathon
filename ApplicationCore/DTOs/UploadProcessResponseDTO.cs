using ApplicationCore.Constants;

namespace ApplicationCore.DTOs
{
    public record UploadProcessReponseDTO
    {
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? StartProcessingDate { get; set; }
        public DateTime? EndProcessingDate { get; set; }
        public EStatusRequestProcessing Status { get; set; }
        public required string FilePath { get; set; }
    }
}
