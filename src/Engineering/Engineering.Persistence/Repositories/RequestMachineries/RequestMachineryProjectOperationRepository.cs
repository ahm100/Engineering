using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryProjectOperationRepository : BaseRepository<EngineeringDBContext, RequestMachineryProjectOperation>, IRequestMachineryProjectOperationRepository
{
    public RequestMachineryProjectOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestMachineryProjectOperation> Data, int RowCount)> GetFilteredProjectOperation(long RequestMachineryId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.ProjectOperation)
                                .ThenInclude(oo => oo.OperationInfo)
                         .Where(oo =>
                                    (oo.RequestMachinery.Id == RequestMachineryId) &&
                                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperation.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperation.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())));

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
