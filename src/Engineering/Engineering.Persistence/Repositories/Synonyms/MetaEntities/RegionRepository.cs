using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewRegionRepository : BaseRepository<EngineeringDBContext, ViewRegion>, IViewRegionRepository
{
    public ViewRegionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewRegion?> GetById(long id, CT ct)
    {
        return await DbSet
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ViewRegion>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.City)
            .Where(oo => ids.Contains(oo.Id) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewRegion>> GetByCodes(List<string> codes, CT ct)
    {
        var query = DbSet
            .Where(oo => codes.Contains(oo.Code) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<ViewRegionDataModel> Data, int RowCount)> GetAllActiveRegionsData(
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.IsActive &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())))
            .Select(x => new ViewRegionDataModel
            {
                Code = x.Code,
                Name = x.Name,
                CityCode = x.City.Code,
                CityName = x.City.Name,
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }
}