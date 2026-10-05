using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRepository : BaseRepository<EngineeringDBContext, Transportation>, ITransportationRepository
{
    public TransportationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<Transportation?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TransportationName == name &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public Task<bool> FindTransportationByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.TransportationName) ||
            codes.Contains(oo.TransportationCode)
            , ct);

        return query;
    }

    public async Task<Transportation?> GetById(long id, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Transportation?> GetByType(TransportationType transportationType, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TransportationType == transportationType);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Transportation?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<Transportation?> FindByCode(string code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.TransportationCode == code &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.TransportationCode)).Select(x => Convert.ToInt64(x.TransportationCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<List<Transportation>> GetsTransportationByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<Transportation> Data, int RowCount)> GetsFilteredTransportation(List<long>? ids, string? filterData, string? code, string? name, bool? isPassenger, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (code == null || oo.TransportationCode.Contains(code)) && (name == null || oo.TransportationName.Contains(name)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TransportationCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TransportationName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        if (isPassenger != null)
            query = query.Where(oo => oo.IsPassenger == isPassenger);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Transportation> Data, int RowCount)> GetsActiveTransportation(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId.Equals(companyId)) &&
            (code == null || oo.TransportationCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TransportationCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.TransportationName, filterData.MakeLikePattern())) &&
            (name == null || oo.TransportationName.Contains(name)) &&
            oo.IsActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

}