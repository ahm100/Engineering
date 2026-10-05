using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryInquiryDocumentRepository : BaseRepository<EngineeringDBContext, RequestMachineryInquiryDocument>, IRequestMachineryInquiryDocumentRepository
{
    public RequestMachineryInquiryDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestMachineryInquiryDocument> Data, int RowCount)> GetFiltered(long RequestMachineryId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => oo.RequestMachineryInquiry.RequestMachineryInquiryOperator.RequestMachinery.Id.Equals(RequestMachineryId));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}
