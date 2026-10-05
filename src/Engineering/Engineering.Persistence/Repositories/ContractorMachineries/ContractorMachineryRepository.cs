using Engineering.Application.Abstractions.Data.ContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;
using Engineering.Domain.Entities.ContractorMachineries.Enums;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Persistence.Repositories.ContractorMachineries;

public class ContractorMachineryRepository : BaseRepository<EngineeringDBContext, ContractorMachinery>, IContractorMachineryRepository
{
    public ContractorMachineryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorMachinery?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery.MachineriesGroup)
            .Where(oo => oo.Id == id && !oo.Machinery.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ContractorMachinery?> IsDuplicate(long contractorId, long MachineryId, ContractorMachineryUnit unit, long? companyId, CT ct)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.ContractorId == contractorId &&
                                                    x.Machinery.Id == MachineryId &&
                                                    x.Unit == unit &&
                                                    (companyId == null || x.CompanyId == companyId));
    }

    public async Task<ContractorMachinery?> GetContractorMachineryForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery.MachineriesGroup)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<(List<ContractorMachinery> Data, int RowCount)> GetsContractorMachineryByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
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

    public async Task<(List<ContractorMachinery> Data, int RowCount)> GetContractorMachineries(List<long>? ids, List<long>? machineryIds, List<long>? contractorIds,
        ContractorMachineryUnit? unit, DateTime? fromDate, DateTime? toDate, string? filterData, bool? isActive, long? companyId, string[]? orderBy,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery.MachineriesGroup)
            .Include(x => x.RequestMachineries)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                        (contractorIds == null || contractorIds.Count == 0 || contractorIds.Contains(oo.ContractorId)) &&
                        (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (unit == null || oo.Unit.Equals(unit)) &&
                        (fromDate == null || oo.Created.Date >= fromDate.Value.Date) &&
                        (toDate == null || oo.Created.Date <= toDate.Value.Date) &&
                        !oo.Machinery.IsDeleted);

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }
    public async Task<(List<ContractorMachinery> Data, int RowCount)> GetsByContractorId(long contractorId, string? filterData, bool? isActive, long? companyId,
        string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        (contractorId.Equals(oo.ContractorId)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        !oo.Machinery.IsDeleted);

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }

    public async Task<(List<ContractorMachinery> Data, int RowCount)> GetActiveContractorMachineries(List<long>? machineryIds, List<long>? contractorIds,
        ContractorMachineryUnit? unit, DateTime? fromDate, DateTime? toDate, string? filterData, long? companyId, string[]? orderBy,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Machinery.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        (contractorIds == null || contractorIds.Count == 0 || contractorIds.Contains(oo.ContractorId)) &&
                        (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (unit == null || oo.Unit.Equals(unit)) &&
                        (fromDate == null || oo.Created.Date >= fromDate.Value.Date) &&
                        (toDate == null || oo.Created.Date <= toDate.Value.Date) &&
                        !oo.Machinery.IsDeleted &&
                        oo.IsActive == true);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }

    public async Task<(List<GetFltrByContractorIdsModel> Data, int RowCount)> GetFltrByContractorIds(
            List<long> contractorIds,
            CT ct)
    {
        var query = DbSet
            .Where(x => contractorIds.Contains(x.ContractorId))
            .Select(x => new GetFltrByContractorIdsModel
            {
                Id = x.Id,
                ContractorId = x.ContractorId,
                MachineryGroupId = x.Machinery.GroupId,
                MachineryGroupName = x.Machinery.MachineriesGroup.GroupName,
                MachineryGroupCode = x.Machinery.MachineriesGroup.GroupCode,
                MachineryId = x.MachineryId,
                MachineryName = x.Machinery.MachineryName,
                MachineryCode = x.Machinery.MachineryCode
            });

        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}