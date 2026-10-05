using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryBillDocumentRepository : BaseRepository<EngineeringDBContext, RequestMachineryBillDocument>, IRequestMachineryBillDocumentRepository
{
    public RequestMachineryBillDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestMachineryBillDocument> Data, int RowCount)> GetFiltered(long requestMachineryId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestMachinery)
                        .Where(oo => oo.RequestMachinery.Id.Equals(requestMachineryId));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
