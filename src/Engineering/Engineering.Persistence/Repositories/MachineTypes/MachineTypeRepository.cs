using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Persistence.Repositories.MachineTypes;

public class MachineTypeRepository : BaseRepository<EngineeringDBContext, MachineType>, IMachineTypeRepository
{
    public MachineTypeRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<MachineType?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.MachineTypeTitle == name);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public Task<bool> FindDuplicateMachineType(
        List<string> names,
        List<string> codes,
        List<int> fromWeights,
        List<int> untilWeights,
        List<int> cabinTypeCodes,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .AnyAsync(oo => (companyId == null || oo.CompanyId == companyId) &&
                            names.Contains(oo.MachineTypeTitle) ||
                            codes.Contains(oo.MachineTypeCode) ||
                            fromWeights.Contains(oo.FromWeight) ||
                            untilWeights.Contains(oo.UntilWeight) ||
                            cabinTypeCodes.Contains(oo.CabinType.CabinTypeCode),
                            ct);

        return query;
    }

    public Task<bool> FindMachineTypeByCodes(List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo => (companyId == null || oo.CompanyId == companyId) &&
            codes.Contains(oo.MachineTypeCode),
            ct);

        return query;
    }

    public async Task<MachineType?> GetDuplicateMachineType(
        string name,
        int fromWeight,
        int untilWeight,
        int cabinTypeCode,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        oo.MachineTypeTitle == name &&
                        oo.FromWeight == fromWeight &&
                        oo.UntilWeight == untilWeight &&
                        oo.CabinType.CabinTypeCode == cabinTypeCode);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<MachineType?> FindByCode(string Code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.MachineTypeCode == Code);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<MachineType?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .Where(oo => oo.Id == id);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<MachineType?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(oo => oo.TransportationRequests)
            .Include(oo => oo.ShippingCosts);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.MachineTypeCode)).Select(x => Convert.ToInt64(x.MachineTypeCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<MachineType> Data, int RowCount)> GetMachineTypes(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .Where(oo => (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (isActive == null || oo.IsActive.Equals(isActive)) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeTitle, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeCode, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<MachineType>> GetsMachineTypeByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<List<MachineType>> GetsMachineTypeByCodes(
        List<string> codes,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => codes.Contains(oo.MachineTypeCode) && oo.CompanyId == companyId);

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<MachineType> Data, int RowCount)> GetsActiveMachineType(
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .Where(oo => oo.IsActive &&
                (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeTitle, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeCode, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<MachineTypeDataModel> Data, int RowCount)> GetsActiveMachineTypeData(
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CabinType)
            .Where(oo => oo.IsActive &&
                (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeTitle, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineTypeCode, filterData.MakeLikePattern())))
            .Select(x => new MachineTypeDataModel
            {
                Code = x.MachineTypeCode,
                Name = x.MachineTypeTitle,
                CabinTypeCode = x.CabinType.CabinTypeCode.ToString(),
                CabinTypeName = x.CabinType.CabinTypeName,
            });

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }
}