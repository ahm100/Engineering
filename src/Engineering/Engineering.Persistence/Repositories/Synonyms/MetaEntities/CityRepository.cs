using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewCityRepository : BaseRepository<EngineeringDBContext, ViewCity>, IViewCityRepository
{
    public ViewCityRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> IsDuplicateName(long? id, string name, CT ct)
    {
        return await DbSet.AnyAsync(x => (id == null || x.Id != id)
                                         && x.Name.ToLower() == name.ToLower(),
            ct);
    }

    public async Task<bool> IsDuplicateEnglishName(long? id, string englishName, CT ct)
    {
        return await DbSet.AnyAsync(x => (id == null || x.Id != id)
                                         && x.EnglishName!.ToLower() == englishName.ToLower(),
            ct);
    }

    public async Task<bool> IsDuplicateCode(long? id, string code, CT ct)
    {
        return await DbSet.AnyAsync(x => (id == null || x.Id != id)
                                         && x.Code == code, ct);
    }

    public async Task<(List<ViewCity> Data, int RowCount)> GetCities(int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewCity> Data, int RowCount)> GetFilteredCities(List<long>? ids, string? code, string? name, long? provinceId,
        string? filterData, bool? isActive, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => (isActive == null || x.IsActive == isActive)
                        && (ids == null || ids.Count == 0 || ids.Contains(x.Id))
                        && (provinceId == null || x.ProvinceId == provinceId)
                        && (string.IsNullOrWhiteSpace(name) || x.Name.Contains(name))
                        && (code == null || x.Code == code)
                        && (string.IsNullOrWhiteSpace(filterData)
                            || EF.Functions.Like(x.Name, filterData.MakeLikePattern())
                            || EF.Functions.Like(x.Code, filterData.MakeLikePattern())));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0)
                     .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (orderBy?.Length > 0)
        {
            query = query.SortBy(orderBy);
        }
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewCity> Data, int RowCount)> GetAllActiveCities(string? filterData, int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.IsActive &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewCityDataModel> Data, int RowCount)> GetAllActiveCitiesData(string? filterData, int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.IsActive &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())))
            .Select(x => new ViewCityDataModel
            {
                Code = x.Code,
                Name = x.Name,
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewCity> Data, int RowCount)> GetCitiesByIds(List<long> ids, string? filterData, bool? ignoreQuery, int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => ids.Contains(x.Id) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
             string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())));

        query = ignoreQuery == true
         ? query.OrderByDescending(x => x.IsActive ? 1 : 0)
                .ThenByDescending(x => x.Created)
                .IgnoreQueryFilters()
         : query.OrderByDescending(x => x.IsActive ? 1 : 0)
                 .ThenByDescending(x => x.Created);

        var count = ignoreQuery == true
            ? await query.IgnoreQueryFilters().CountAsync(ct)
            : await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<ViewCity?> GetById(long id, CT ct)
    {
        var provinceId = DbSet.Where(x => x.Id == id).ToQueryString();
        return await DbSet
            .Include(x => x.Regions)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<MyViewCity?> GetCityById(
        long id, CT ct)
    {
        var provinceId = DbSet.Where(x => x.Id == id).ToQueryString();
        return await DbSet
            .Select(x => new MyViewCity
            {
                Id = x.Id,
                Name = x.Name,
                ProvinceId = x.ProvinceId,

            })
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ViewCity>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.Regions)
            .Where(oo => ids.Contains(oo.Id) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }


    public async Task<List<ViewCity>> GetByCodes(List<string> codes, CT ct)
    {
        var query = DbSet.Where(oo => codes.Contains(oo.Code) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }
}