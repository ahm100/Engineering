using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryInquiryDocumentRepository : IBaseRepository<RequestMachineryInquiryDocument>
{
    Task<(List<RequestMachineryInquiryDocument> Data, int RowCount)> GetFiltered(long RequestMachineryId,
                                                                                string[]? orderBy,
                                                                                int pageIndex,
                                                                                int pageSize,
                                                                                CT ct);
}
