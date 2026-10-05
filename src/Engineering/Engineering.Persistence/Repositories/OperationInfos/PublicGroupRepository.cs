using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class PublicGroupRepository : BaseRepository<EngineeringDBContext, PublicGroup>, IPublicGroupRepository
{
    public PublicGroupRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<PublicGroup?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<PublicGroup> Data, int RowCount)> GetsPublicGroupFiltered(List<long>? productGroupIds, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo =>
            (productGroupIds == null || productGroupIds.Contains(oo.ProductGroupId)));

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}