using ApplicationCore.Constants;

namespace ApplicationCore.Entities
{
    public class RequestProcessing : BaseEntity
    {
        public RequestProcessing(string requestFilePath)
        {
            Id = Guid.NewGuid();
            RequestFilePath = requestFilePath;
            DateCreate = DateTime.Now;
            Status = EStatusRequestProcessing.NotProcessed;
        }

        public string RequestFilePath { get; private set; }
        public DateTime DateCreate { get; private set; }
        public DateTime DateStartProcessing { get; private set; }
        public DateTime DateEndProcessing { get; private set; }
        public EStatusRequestProcessing Status { get; private set; }
        public string? FilePath { get; private set; }

        public void StartProcessing()
        {
            DateStartProcessing = DateTime.Now;
            Status = EStatusRequestProcessing.Processing;
        }

        public void EndProcessing(string filePath)
        {
            DateEndProcessing = DateTime.Now;
            Status = EStatusRequestProcessing.Processed;
            FilePath = filePath;
        }
    }
}
