using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class RequestProcessingSpecification : Specification<RequestProcessing>, ISingleResultSpecification<RequestProcessing>
    {
        public RequestProcessingSpecification(EStatusRequestProcessing eStatusRequestProcessing)
        {
            Query.
                Where(u => u.Status == eStatusRequestProcessing);
        }
    }
}
