using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewAddressRepository : BaseRepository<EngineeringDBContext, ViewAddress>, IViewAddressRepository
{
    public ViewAddressRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> IsDuplicateAddress(long? id, long thirdPartyId, string title,
        CT ct)
    {
        return await DbSet.AnyAsync(x => (id == null || x.Id != id)
                                         && x.ThirdPartyId == thirdPartyId
                                         && x.Title == title,
            ct);
    }

    public async Task<(List<ViewAddress> Data, int RowCount)> GetActiveAddresses(int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => x.IsActive);

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0)
          .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewAddress> Data, int RowCount)> GetActiveAddressesByThirdPartyId(long thirdPartyId,
        bool? isActive, bool? isDefault,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.City)
            .Where(x => (isActive == null || x.IsActive == isActive)
                        && (isDefault == null || x.IsDefault == isDefault)
                        && x.ThirdPartyId == thirdPartyId);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewAddress> Data, int RowCount)> GetFilteredAddresses(long? thirdPartyId, string? fullName,
        string? organizationCode, string? filterData, bool? isActive, string[]? orderBy, int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x =>
            (isActive == null || x.IsActive == isActive)
            && (thirdPartyId == null || x.ThirdPartyId == thirdPartyId)
            && (string.IsNullOrWhiteSpace(fullName) ||
                string.Concat(x.ThirdParty.FirstName, " ", x.ThirdParty.LastName).Contains(fullName)) &&
            (string.IsNullOrWhiteSpace(organizationCode) || x.ThirdParty.OrganizationCode == organizationCode) &&
            (string.IsNullOrWhiteSpace(filterData) ||
             EF.Functions.Like(x.ThirdParty.OrganizationCode!, filterData.MakeLikePattern()) ||
             EF.Functions.Like(string.Concat(x.ThirdParty.FirstName, " ", x.ThirdParty.LastName),
                 filterData.MakeLikePattern())));

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

    public async Task<(List<ViewAddress> Data, int RowCount)> GetAllActiveAddresses(string? filterData, int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x =>
            x.IsActive
            && (string.IsNullOrWhiteSpace(filterData)
                || x.ThirdParty.OrganizationCode!.Contains(filterData)
                || string.Concat(x.ThirdParty.FirstName, " ", x.ThirdParty.LastName).Contains(filterData)));

        query = query.OrderByDescending(x => x.IsActive ? 1 : 0)
                     .ThenByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<ViewAddress> Data, int RowCount)> GetAddressesByIds(List<long> ids, bool? ignoreQuery,
        int pageIndex, int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => ids.Contains(x.Id));

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

    public async Task<ViewAddress?> GetDefaultAddressByThirdPartyId(long thirdPartyId, long? ViewAddressId,
           CT ct)
    {
        var query = await DbSet.FirstOrDefaultAsync(x => x.ThirdPartyId == thirdPartyId
                                                         && (ViewAddressId == null || x.Id != ViewAddressId)
                                                         && x.IsDefault == true, ct);
        return query;
    }

    public async Task<ViewAddress?> GetById(long id, CT ct)
    {
        return await DbSet
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ViewAddress>> GetByIds(List<long> ids, CT ct)
    {
        var query = DbSet.Where(oo => ids.Contains(oo.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<List<long>> GetThirdPartyIdsByProvinceAndCity(
        long? provinceId,
        long? cityId,
        CT ct)
    {
        var query = DbSet.Where(x =>
            (provinceId == null || x.City!.ProvinceId == provinceId) &&
            (cityId == null || x.CityId == cityId) &&
            (x.ThirdPartyId != null && x.ThirdPartyId > 0)
        ).Select(x => x.ThirdPartyId!.Value);

        return await query.ToListAsync(ct);
    }
}