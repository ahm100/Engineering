using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Persistence.Repositories.Trips;

public class TripRepository : BaseRepository<EngineeringDBContext, Trip>, ITripRepository
{
    public TripRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<Trip?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TripName == name &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public Task<bool> FindTripByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.TripName) ||
            codes.Contains(oo.TripCode)
            , ct);

        return query;
    }

    public async Task<Trip?> GetById(long id, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Trip?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.TransportationRequests)
             .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Trip?> FindByCode(string code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TripCode == code &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<List<Trip>?> GetByCodes(List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => codes.Contains(oo.TripCode) &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var item = await query.ToListAsync(ct);
        return item;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.TripCode)).Select(x => Convert.ToInt64(x.TripCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<Trip> Data, int RowCount)> GetsFilteredTrip(List<long>? ids, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId.Equals(companyId)) &&
            (code == null || oo.TripCode.Contains(code)) &&
            (name == null || oo.TripName.Contains(name)) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TripCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TripName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Trip> Data, int RowCount)> GetsActiveTrip(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId.Equals(companyId)) &&
            (code == null || oo.TripCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TripCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TripName, filterData.MakeLikePattern())) &&
            (name == null || oo.TripName.Contains(name)) &&
            oo.IsActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Trip>> GetsTripByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

}