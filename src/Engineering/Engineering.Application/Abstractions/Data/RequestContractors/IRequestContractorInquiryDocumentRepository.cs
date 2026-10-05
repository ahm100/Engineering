using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Abstractions.Data.RequestContractors;

public interface IRequestContractorInquiryDocumentRepository : IBaseRepository<RequestContractorInquiryDocument>
{
    Task<(List<RequestContractorInquiryDocument> Data, int RowCount)> GetFiltered(
        long requestContractorInquiryId,
        int pageIndex,
        int pageSize,
        CT ct);
}
