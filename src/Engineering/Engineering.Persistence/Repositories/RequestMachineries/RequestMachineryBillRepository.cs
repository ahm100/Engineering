using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryBillRepository : BaseRepository<EngineeringDBContext, RequestMachineryBill>, IRequestMachineryBillRepository
{
    public RequestMachineryBillRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<long> BillNumberCreator(CT ct)
    {
        var query = await DbSet
            .Where(x => (x.BillNumber > 0))
            .Select(x => (long)x.BillNumber!).ToListAsync(ct);

        long suggestedBillNumber = 1;
        if (query is not null && query.Any())
            suggestedBillNumber = query.Max() + 1;

        return suggestedBillNumber;
    }
    public async Task<RequestMachineryBill?> GetByIdAsync(long RequestMachineryBillId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestMachinery.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestMachinery.Machinery.MachineriesGroup)
            .Include(oo => oo.RequestMachinery.ProjectOperations)
                .ThenInclude(x => x.ProjectOperation.OperationInfo)
            .Include(oo => oo.RequestMachinery.ProjectOperationDetails)
                .ThenInclude(x => x.ProjectOperationDetail.OperationLocation)
            .Where(oo => oo.Id.Equals(RequestMachineryBillId))
            .OrderByDescending(oo => oo.Created);

        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<List<RequestMachineryBill>?> GetByIdsAsync(List<long> RequestMachineryBillIds, CT ct)
    {
        var query = DbSet
                         .Where(oo => RequestMachineryBillIds.Contains(oo.Id))
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<RequestMachineryBill> Data, int RowCount)> GetFilteredAsync(List<long>? ids, long? requestMachineryId,
        long? costCenterId, long? projectId, List<long>? contractorIds, List<long>? projectOperationIds, long? machineriesGroupId,
        long? machineryId, DateTime? fromDate, DateTime? toDate, long? creatorId, int? billNumber, string? filterData, long? companyId,
        string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestMachinery.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestMachinery.Machinery.MachineriesGroup)
            .Include(oo => oo.RequestMachinery.ProjectOperations)
                .ThenInclude(x => x.ProjectOperation.OperationInfo)
            .Include(oo => oo.RequestMachinery.ProjectOperationDetails)
                .ThenInclude(x => x.ProjectOperationDetail.OperationLocation)
                         .Where(oo =>
                         (companyId == null || oo.RequestMachinery.CompanyId == companyId) &&
                         (requestMachineryId == null || oo.RequestMachinery.Id == requestMachineryId) &&
                         (contractorIds == null || (oo.ContractorId.HasValue && contractorIds.Contains(oo.ContractorId.Value))) &&
                         (costCenterId == null || oo.RequestMachinery.Project!.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                         (projectId == null || oo.RequestMachinery.Project!.Id == projectId) &&
                         (billNumber == null || oo.BillNumber == billNumber) &&
                         (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                         (filterData == null || string.IsNullOrEmpty(filterData) ||
                         EF.Functions.Like(oo.BillNumber.ToString(), filterData.MakeLikePattern()) ||
                         EF.Functions.Like(oo.RequestMachinery.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                         (machineriesGroupId == null || oo.RequestMachinery.Machinery.MachineriesGroup!.Id == machineriesGroupId) &&
                         (machineryId == null || oo.RequestMachinery.Machinery!.Id == machineryId) &&
                         (fromDate == null || (oo.FromDate != null && oo.FromDate.Value.Date >= fromDate.Value.Date)) &&
                         (toDate == null || (oo.ToDate != null && oo.ToDate.Value.Date <= toDate.Value.Date)) &&
                         (projectOperationIds == null || oo.RequestMachinery.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                         (creatorId == null || oo.CreatorId.Equals(creatorId)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<bool?> IsDuplicateBill(long requestMachineryId, DateTime fromDate, DateTime toDate, CT ct)
        => await DbSet.AnyAsync(x => x.RequestMachinery.Id == requestMachineryId && x.FromDate == fromDate && x.ToDate == toDate, ct);

    public async Task<List<string?>?> GetSupplierDrivers(long? supplierId, string? filterData, CT ct)
    {
        var query = DbSet.Where(x => (supplierId == null || x.RequestMachinery.ContractorId == supplierId) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) ||
                                     EF.Functions.Like(x.DriverName, filterData.MakeLikePattern())) &&
                                     !string.IsNullOrEmpty(x.DriverName))
                         .Select(x => x.DriverName);

        var items = await query.Distinct().ToListAsync(ct);

        return items;
    }
}
