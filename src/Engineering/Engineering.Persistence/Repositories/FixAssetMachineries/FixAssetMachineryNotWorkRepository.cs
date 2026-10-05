using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class FixAssetMachineryNotWorkRepository : BaseRepository<EngineeringDBContext, FixAssetMachineryNotWork>, IFixAssetMachineryNotWorkRepository
{
    public FixAssetMachineryNotWorkRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<FixAssetMachineryNotWork?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.FixAssetNotWorkDocuments)
            .Include(x => x.FixAssetMachinery)
                .ThenInclude(x => x.Machinery)
            .Where(oo => oo.Id == id &&
                        !oo.FixAssetMachinery.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<FixAssetMachineryNotWork?> GetFixAssetMachineryNotWorkForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.FixAssetMachinery)
                .ThenInclude(x => x.Machinery)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<FixAssetMachineryNotWork> Data, int RowCount)> GetsFixAssetMachineryNotWorkByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<FixAssetMachineryNotWork> Data, int RowCount)> GetFixAssetMachineryNotWorks(
        List<long>? fixAssetMachineryIds,
        List<long>? machineryIds,
        FixAssetMachineryType? type,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
              .Include(x => x.FixAssetNotWorkDocuments)
              .Include(x => x.FixAssetMachinery)
                .ThenInclude(x => x.Machinery)
            .Where(oo =>
                        (machineryIds == null || machineryIds.Contains(oo.FixAssetMachinery.Machinery.Id)) &&
                        (fixAssetMachineryIds == null || fixAssetMachineryIds.Contains(oo.FixAssetMachinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (type == null || oo.FixAssetMachinery.FixAssetMachineryType.Equals(type)) &&
                        (fromDate == null || oo.StartDate >= fromDate) &&
                        (toDate == null || oo.EndDate <= toDate) &&
                        !oo.FixAssetMachinery.IsDeleted);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }
    public async Task<(List<GetsFixAssetMachineryNotWorkExcelExporterModel> Data, int RowCount)> GetsFixAssetMachineryNotWorkForExcel(
        List<long>? ids,
        List<long>? notWorkIds,
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
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                        (machineryIds == null || machineryIds.Contains(oo.FixAssetMachinery.Machinery.Id)) &&
                        (ids == null || ids.Contains(oo.FixAssetMachinery.Id)) &&
                        (notWorkIds == null || notWorkIds.Contains(oo.Id)) &&
                        (contractorIds == null || (oo.FixAssetMachinery.ContractorId.HasValue && contractorIds.Contains(oo.FixAssetMachinery.ContractorId.Value))) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (string.IsNullOrWhiteSpace(numberPlates) || EF.Functions.Like(oo.FixAssetMachinery.MachinerySpecification, numberPlates.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(numberPlates) || EF.Functions.Like(oo.FixAssetMachinery.NumberPlates, numberPlates.MakeLikePattern())) &&
                        (type == null || oo.FixAssetMachinery.FixAssetMachineryType.Equals(type)) &&
                        (fromDate == null || oo.FixAssetMachinery.StartDate >= fromDate) &&
                        (toDate == null || oo.FixAssetMachinery.EndDate <= toDate) &&
                        !oo.FixAssetMachinery.IsDeleted);

        if (isActive != null)
            query = query.Where(x => x.FixAssetMachinery.IsActive == isActive);

        if (!string.IsNullOrEmpty(driverFilter) && driverIds is not null && driverIds.Any() && driverIds.Count > 0)
            query = query.Where(x => EF.Functions.Like(x.FixAssetMachinery.DriverName, driverFilter.MakeLikePattern()) || x.FixAssetMachinery.DriverId.HasValue && driverIds.Contains(x.FixAssetMachinery.DriverId!.Value));
        else if (!string.IsNullOrEmpty(driverFilter))
            query = query.Where(x => EF.Functions.Like(x.FixAssetMachinery.DriverName, driverFilter.MakeLikePattern()));
        else if (driverIds is not null && driverIds.Any() && driverIds.Count > 0)
            query = query.Where(x => x.FixAssetMachinery.DriverId.HasValue && driverIds.Contains(x.FixAssetMachinery.DriverId!.Value));

        var data = query.Select(x => new GetsFixAssetMachineryNotWorkExcelExporterModel()
        {
            ContractorId = x.FixAssetMachinery.ContractorId,
            Created = x.Created,
            CreatorId = x.CreatorId,
            Description = x.Description,
            FixAssetMachineryId = x.FixAssetMachinery.Id,
            FixAssetMachineryType = x.FixAssetMachinery.FixAssetMachineryType,
            FromDate = x.StartDate,
            ToDate = x.EndDate,
            Id = x.Id,
            MachineryCode = x.FixAssetMachinery.Machinery.MachineryCode,
            MachineryName = x.FixAssetMachinery.Machinery.MachineryName,
            CompanyId = x.FixAssetMachinery.CompanyId
        });

        data = data.OrderByDescending(oo => oo.Created);

        var count = await data.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            data = data.Page(pageIndex, pageSize);

        var notWorks = await data.ToListAsync(ct);

        return (notWorks, count);
    }

}