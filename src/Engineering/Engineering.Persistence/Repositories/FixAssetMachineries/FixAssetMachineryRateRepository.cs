using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class FixAssetMachineryRateRepository : BaseRepository<EngineeringDBContext, FixAssetMachineryRate>, IFixAssetMachineryRateRepository
{
    public FixAssetMachineryRateRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<FixAssetMachineryRate?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.FixAssetMachinery)
                .ThenInclude(x => x.Machinery)
            .Where(oo => oo.Id == id &&
                        !oo.FixAssetMachinery.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<FixAssetMachineryRate> Data, int RowCount)> GetsRateByFixAssetMachineryId(
        long fixAssetMachineryId,
        DateTime? fromDate,
        DateTime? toDate,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
              .Include(x => x.FixAssetMachinery)
                .ThenInclude(x => x.Machinery)
            .Where(oo =>
                        (oo.FixAssetMachinery.Id == fixAssetMachineryId) &&
                        (fromDate == null || oo.StartDate >= fromDate) &&
                        (toDate == null || oo.EndDate <= toDate) &&
                        !oo.FixAssetMachinery.IsDeleted);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}