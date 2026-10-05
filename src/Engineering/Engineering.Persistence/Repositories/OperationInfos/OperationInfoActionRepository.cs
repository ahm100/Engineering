using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using global::Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoActionRepository : BaseRepository<EngineeringDBContext, OperationInfoAction>, IOperationInfoActionRepository
{
    public OperationInfoActionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetOperationInfoActionModel> Data, int RowCount)> GetsOperationInfoActions(
    long oInfoId,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
            x.OperationInfoId == oInfoId &&
            (
            string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.Action.ActionCode, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.Action.ActionName, filterData.MakeLikePattern())
            ))
            .Select(x => new GetOperationInfoActionModel(
                x.Id,
                x.ActionId,
                x.Action.ActionName,
                x.Action.ActionCode,
                x.Price
            ));


        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query
        .ToListAsync(ct);

        var rowCount = await query.CountAsync(ct);
        return (data, rowCount);
    }

    public async Task<List<OperationInfoAction>> GetOperationInfoActions(
    List<long> oInfoIds,
    CT ct)
    {
        var query = DbSet
            .Where(x => oInfoIds.Contains(x.OperationInfoId));
        var data = await query
        .ToListAsync(ct);

        return data;
    }

    public async Task<List<GetOIActionByOperationInfoIdModel>> GetOIActionByOperationInfoId(
    long oInfoId,
    CT ct)
    {
        return await DbSet
        .Where(x => x.OperationInfoId == oInfoId)
        .Select(x => new GetOIActionByOperationInfoIdModel
        {
            Id = x.Id,
            ActionId = x.ActionId,
            ActionName = x.Action.ActionName,
            ActionCode = x.Action.ActionCode
        })
        .ToListAsync(ct);
    }

    public async Task<List<OperationInfoAction>> GetOperationInfoActionsWithActionIds(
    long oInfoId,
    List<long> actionsIds,
    CT ct)
    {
        var query = DbSet
            .Where(x => x.OperationInfoId == oInfoId && actionsIds.Contains(x.ActionId));
        var data = await query
        .ToListAsync(ct);

        return data;
    }

    public async Task<List<OperationInfoAction>> GetOIActionByOIId(
    long oInfoId,
    CT ct)
    {
        var query = DbSet
            .Where(x => x.OperationInfoId == oInfoId);
        var data = await query
        .ToListAsync(ct);

        return data;
    }

    public async Task<OperationInfoAction> GetOperationInfoActionByActionId(
    long oInfoId,
    long actionId,
    CT ct)
    {
        var query = await DbSet
            .FirstOrDefaultAsync(
                x => x.OperationInfoId == oInfoId &&
                     x.ActionId == actionId,
                ct);

        return query;
    }

    public async Task<List<OperationInfoAction>> GetOperationInfoActionByActionId(
    List<long> oInfoActionIds,
    CT ct)
    {
        var query = DbSet
            .Where(
                x => oInfoActionIds.Contains(x.Id));

        return await query.ToListAsync(ct);
    }

}

