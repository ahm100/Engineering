using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class FixAssetMachineryRepository : BaseRepository<EngineeringDBContext, FixAssetMachinery>, IFixAssetMachineryRepository
{
    public FixAssetMachineryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<FixAssetMachinery?> GetById(
        long id,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery)
            .Include(x => x.FixAssetMachineryRates)
            .Include(x => x.FixAssetMachineryNotWorks)
            .Include(x => x.FixAssetMachineryDocuments)
            .Include(x => x.MachineryReservations)
                .ThenInclude(x => x.RequestMachinery)
            .Where(oo => oo.Id == id &&
                        (companyId == null || oo.CompanyId == companyId) &&
                        !oo.Machinery.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<FixAssetMachinery?> GetFixAssetMachineryForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery)
            .Include(x => x.FixAssetMachineryNotWorks)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<(List<FixAssetMachinery> Data, int RowCount)> GetsFixAssetMachineryByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<FixAssetMachinery> Data, int RowCount)> GetFixAssetMachineries(
        List<long>? ids,
        List<long>? machineryIds,
        List<long>? contractorIds,
        FixAssetMachineryType? type,
        string? numberPlates,
        DateTime? fromDate,
        DateTime? toDate,
        List<long>? driverIds,
        string? driverFilter,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery)
            .Include(x => x.FixAssetMachineryDocuments)
            .Include(x => x.FixAssetMachineryNotWorks)
            .Include(x => x.FixAssetMachineryRates)
            .Include(x => x.MachineryReservations)
                .ThenInclude(x => x.RequestMachinery)
            .Where(f => (companyId == null || f.CompanyId == companyId) &&
                        (ids == null || ids.Count == 0 || ids.Contains(f.Id)) &&
                        (contractorIds == null || contractorIds.Count == 0 || (f.ContractorId.HasValue && contractorIds.Contains(f.ContractorId.Value))) &&
                        (machineryIds == null || machineryIds.Contains(f.Machinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(f.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(f.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (string.IsNullOrWhiteSpace(numberPlates) || EF.Functions.Like(f.NumberPlates, numberPlates.MakeLikePattern())) &&
                        (type == null || f.FixAssetMachineryType.Equals(type)) &&
                        (fromDate == null || f.StartDate >= fromDate) &&
                        (toDate == null || f.EndDate <= toDate) &&
                        !f.Machinery.IsDeleted);

        if (isActive != null)
            query = query.Where(f => f.IsActive == isActive);

        if (!string.IsNullOrEmpty(driverFilter) && driverIds is not null && driverIds.Any() && driverIds.Count > 0)
            query = query.Where(x => EF.Functions.Like(x.DriverName, driverFilter.MakeLikePattern()) || x.DriverId.HasValue && driverIds.Contains(x.DriverId!.Value));
        else if (!string.IsNullOrEmpty(driverFilter))
            query = query.Where(x => EF.Functions.Like(x.DriverName, driverFilter.MakeLikePattern()));
        else if (driverIds is not null && driverIds.Any() && driverIds.Count > 0)
            query = query.Where(x => x.DriverId.HasValue && driverIds.Contains(x.DriverId!.Value));

        query = query.OrderBy(f => !f.IsActive).ThenByDescending(f => f.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }

    public async Task<List<long>?> GetFixAssetMachineryDriverIds(CT ct)
    {
        var query = DbSet
            .Where(f => f.DriverId != null && f.DriverId > 0)
            .Select(f => (long)f.DriverId!);

        var items = await query.Distinct().ToListAsync(ct);

        return items;
    }

    public async Task<(List<FixAssetMachinery> Data, int RowCount)> GetActiveFixAssetMachineries(
        List<long>? machineryIds,
        FixAssetMachineryType? type,
        string? numberPlates,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery)
            .Include(x => x.FixAssetMachineryNotWorks)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (string.IsNullOrWhiteSpace(numberPlates) || EF.Functions.Like(oo.NumberPlates, numberPlates.MakeLikePattern())) &&
                        (type == null || oo.FixAssetMachineryType.Equals(type)) &&
                        oo.IsActive &&
                        !oo.Machinery.IsDeleted &&
                        oo.Machinery.IsActive);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }
}