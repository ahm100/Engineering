using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoSeasonRepository : BaseRepository<EngineeringDBContext, OperationInfoSeason>, IOperationInfoSeasonRepository
{
    public OperationInfoSeasonRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<OperationInfoSeason> Data, int RowCount)> GetsByOprationInfoId(
        long oprationInfoId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.Season.Branch.Category)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoSeason> Data, int RowCount)> GetByOprationInfoIds(
        List<long>? oprationInfoIds,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.Season.Branch.Category)

            .Where(oo => oprationInfoIds.Contains(oo.OperationInfo.Id));

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoSeason> Data, int RowCount)> GetsOperationInfoSeasonByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.Season.Branch.Category)
            .Include(x => x.RequestGoodsSupplies)

            .Where(x =>
                x.Season.IsActive &&
                x.Season.Branch.IsActive &&
                x.Season.Branch.Category.IsActive &&
                x.OperationInfo.ProjectOperations.Any(p =>
                p.Id.Equals(projectOperationId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<OperationInfoSeason?> GetById(long seasonId, long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Season)
                .ThenInclude(x => x.Branch)
                    .ThenInclude(x => x.Category)

            .Where(oo => oo.Season.Id == seasonId && oo.OperationInfo.Id == oprationInfoId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<OperationInfoSeason>?> GetBySeasonAndOI(
        long seasonId,
        long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Season)
                .ThenInclude(x => x.Branch)
                    .ThenInclude(x => x.Category)

            .Where(oo => oo.Season.Id == seasonId && oo.OperationInfo.Id == oprationInfoId);

        return await query.ToListAsync(ct);
    }

    public async Task<List<OperationInfoSeason>?> GetBySeasonIdsAndOIIds(
        List<long> seasonIds,
        List<long> oprationInfoIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.Season)
                .ThenInclude(x => x.Branch)
                    .ThenInclude(x => x.Category)
            .Where(oo => seasonIds.Contains(oo.Season.Id) && oprationInfoIds.Contains(oo.OperationInfo.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<OperationInfoSeason?> GetOperationInfoSeasonById(long Id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.Season)
                .ThenInclude(x => x.Branch)
                    .ThenInclude(x => x.Category)

            .Where(oo => oo.Id.Equals(Id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<OperationInfoSeason?> GetOperationInfoSeasonByIdForDelete(long Id, CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestGoodsSupplies)

            .Where(oo => oo.Id.Equals(Id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<OperationInfoSeason> Data, int RowCount)> GetsOperationInfoSeasonFiltered(long? categoryId, long? brnachId, long? seasonId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Include(x => x.Season.Branch.Category)
            .Include(x => x.OperationInfo)
                .ThenInclude(x => x.OperationInfoSeasons)
                    .ThenInclude(x => x.Season.Branch.Category)

            .Where(oo =>
            (categoryId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) ||
            (brnachId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == brnachId)) ||
            (seasonId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId))
            );

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