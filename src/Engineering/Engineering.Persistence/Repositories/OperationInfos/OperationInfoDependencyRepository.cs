using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoDependencyRepository : BaseRepository<EngineeringDBContext, OperationInfoDependency>, IOperationInfoDependencyRepository
{
    public OperationInfoDependencyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<OperationInfoDependency?> GetOperationInfoDependence(long operationInfoId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
                .Where(oo => oo.RelationId == operationInfoId);

        var result = await query
            .FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<OperationInfoDependency> Data, int RowCount)> GetsDependentOnOperationInfo(long operationInfoId, CT ct)
    {
        var query = DbSet
                .Where(oo => oo.OperationInfo.Id == operationInfoId)
                .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query
            .ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoDependency> Data, int RowCount)> FindOperationInfoDependencies(long operationInfoId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Where(oo => oo.OperationInfo.Id == operationInfoId)
            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query
            .ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoDependency> Data, int RowCount)> GetOperationInfoDependencies(long operationInfoId, OperationInfoDependencyType? dependencyType, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Where(oo => oo.OperationInfo.Id == operationInfoId &&
            (dependencyType == null || oo.DependencyType == dependencyType)
            );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<OperationInfoDependency?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
           .Where(oo => oo.Id == id)
           .Include(x => x.OperationInfo);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }
}