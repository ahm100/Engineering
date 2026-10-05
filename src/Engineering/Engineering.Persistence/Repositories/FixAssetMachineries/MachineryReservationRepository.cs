using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class MachineryReservationRepository : BaseRepository<EngineeringDBContext, MachineryReservation>, IMachineryReservationRepository
{
    public MachineryReservationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<MachineryReservation?> GetById(long id, long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestMachinery.Machinery)
            .Include(x => x.RequestMachinery.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Include(x => x.FixAssetMachinery.MachineryReservations)
            .Include(x => x.FixAssetMachinery.Machinery)
            .Where(oo => oo.Id == id && !oo.FixAssetMachinery.Machinery.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<MachineryReservation?> GetMachineryReservationForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestMachinery.Machinery)
            .Include(x => x.RequestMachinery.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Include(x => x.FixAssetMachinery.Machinery)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }


    public async Task<(List<MachineryReservation> Data, int RowCount)> GetsMachineryReservationByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
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

    public async Task<(List<MachineryReservation> Data, int RowCount)> GetMachineryReservations(
        List<long>? ids,
        List<long>? machineryIds,
        List<long>? fixAssetMachineryIds,
        List<long>? requestMachineryIds,
        MachineryReservationUnit? unit,
        MachineryReservationStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestMachinery.Machinery)
            .Include(x => x.RequestMachinery.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Include(x => x.FixAssetMachinery.Machinery)
            .Where(oo =>
                        (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                        (machineryIds == null || machineryIds.Contains(oo.FixAssetMachinery.Machinery.Id)) &&
                        (fixAssetMachineryIds == null || fixAssetMachineryIds.Contains(oo.FixAssetMachinery.Id)) &&
                        (requestMachineryIds == null || requestMachineryIds.Contains(oo.RequestMachinery.Id)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.FixAssetMachinery.Machinery.MachineryCode, filterData.MakeLikePattern())) &&
                        (unit == null || oo.Unit.Equals(unit)) &&
                        (status == null || oo.Status.Equals(status)) &&
                        (fromDate == null || oo.StartDate.Date >= fromDate.Value.Date) &&
                        (toDate == null || oo.EndDate.Date <= toDate.Value.Date) &&
                        !oo.FixAssetMachinery.Machinery.IsDeleted);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }


}