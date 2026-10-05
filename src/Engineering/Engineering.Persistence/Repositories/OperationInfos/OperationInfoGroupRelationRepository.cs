using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoGroupRelationRepository : BaseRepository<EngineeringDBContext, OperationInfoGroupRelation>, IOperationInfoGroupRelationRepository
{
    public OperationInfoGroupRelationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsByOprationInfoId(long oprationInfoId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.OperationInfoGroup)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsByOprationInfoGroupId(long operationInfoGroupId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.OperationInfoGroup)

            .Where(oo => oo.OperationInfoGroup.Id == operationInfoGroupId);

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<OperationInfoGroupRelation?> GetById(long operationInfoGroupId, long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoGroup)
            .Include(x => x.OperationInfo)

            .Where(oo => oo.OperationInfoGroup.Id == operationInfoGroupId && oo.OperationInfo.Id == oprationInfoId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<OperationInfoGroupRelation> Data, int RowCount)> GetsOperationInfoGroupRelationFiltered(long? operationInfoId, long? operationInfoGroupId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoGroup)
            .Include(x => x.OperationInfo)
                .ThenInclude(x => x.OperationInfoGroupRelations)
                    .ThenInclude(x => x.OperationInfoGroup)

            .Where(oo =>
            (operationInfoId == null || oo.OperationInfo.OperationInfoGroupRelations.Any(x => x.OperationInfo.Id == operationInfoId)) ||
            (operationInfoGroupId == null || oo.OperationInfo.OperationInfoGroupRelations.Any(x => x.OperationInfoGroup.Id == operationInfoGroupId))
            );

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}