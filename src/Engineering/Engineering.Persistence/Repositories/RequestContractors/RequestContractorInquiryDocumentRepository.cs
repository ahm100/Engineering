using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Persistence.Repositories.RequestContractors;

public class RequestContractorInquiryDocumentRepository : BaseRepository<EngineeringDBContext, RequestContractorInquiryDocument>, IRequestContractorInquiryDocumentRepository
{
    public RequestContractorInquiryDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestContractorInquiryDocument> Data, int RowCount)> GetFiltered(
        long requestContractorInquiryId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => x.RequestContractorInquiry.RequestContractor.Id.Equals(requestContractorInquiryId));

        query = query.OrderByDescending(x => x.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}
