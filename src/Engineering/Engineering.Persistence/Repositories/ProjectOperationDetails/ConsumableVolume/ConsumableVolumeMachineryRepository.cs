using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.ConsumableVolume;

public class ConsumableVolumeMachineryRepository : BaseRepository<EngineeringDBContext, ConsumableVolumeMachinery>, IConsumableVolumeMachineryRepository
{
    public ConsumableVolumeMachineryRepository(EngineeringDBContext context) : base(context) { }

    public async Task<ConsumableVolumeMachinery?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetail)
                .ThenInclude(x => x.ProjectOperation)
                    .ThenInclude(x => x.OperationInfo)
                        .ThenInclude(x => x.ConsumptionStandardMachineries)
            .Where(oo => oo.Id == id && !oo.ProjectOperationDetail.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ConsumableVolumeMachinery> Data, int RowCount)> GetsByProjectOperationDetailId(long id, string? filterData, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectOperationDetail!.Id == id && (filterData == null ||
                                                                               string.IsNullOrEmpty(filterData) ||
                                                                               EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                                                                               EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())))
            .Include(oo => oo.ProjectOperationDetail)
            .Include(oo => oo.Machinery);

        var count = await query.CountAsync(ct);
        var items = await query.Page(pageIndex, pageSize).ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ConsumableVolumeMachinery> Data, int RowCount)> GetsFilteredMachineriyVolume(long projectId, long? projectOperationId, long? projectOperationDetailId,
        long? machineryGroupId, long? machineryId, string? filterData, CT ct)
    {
        var query = DbSet
            .Where(oo =>
                oo.ProjectOperationDetail!.ProjectOperation.Project.Id == projectId &&
                (projectOperationId == null || oo.ProjectOperationDetail!.ProjectOperation.Id == projectOperationId) &&
                (projectOperationDetailId == null || oo.ProjectOperationDetail!.Id == projectOperationDetailId) &&
                (machineryGroupId == null || oo.Machinery.MachineriesGroup!.Id == machineryGroupId) &&
                (machineryId == null || oo.Machinery!.Id == machineryId) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(oo.Machinery.MachineryName, filterData.MakeLikePattern()) ||
                EF.Functions.Like(oo.Machinery.MachineryCode, filterData.MakeLikePattern())));

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<MachineriesDataModel> Data, int RowCount)> GetsByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectOperationDetail!.ProjectOperation.Id == projectOperationId && oo.IsDeleted != true)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo.ConsumableVolumeMachineries)
                .ThenInclude(oo => oo.Machinery)
            .Select(e => new MachineriesDataModel
            {
                ProjectOperationDetailMachineryId = e.Id,
                ProjectOperationDetailId = e.ProjectOperationDetail.Id,
                Id = e.Machinery.Id,
                MachineryName = e.Machinery.MachineryName,
                Number = e.Number,
                UnusedPercentage = e.UnusedPercentage,
                IsStandard = e.IsStandard,
                StandardValue = e.StandardValue,
                FinalValue = e.FinalValue
            });


        var count = await query.CountAsync(ct);
        var items = await query.Page(pageIndex, pageSize).ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ConsumableVolumeMachinery>> GetFilteredTotalOfConsumebleMachineries(
        long machineryId,
        long projectId,
        long costCenterId,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                        oo.Machinery.Id == machineryId &&
                        oo.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                        oo.ProjectOperationDetail.ProjectOperation.Project.Id == projectId &&
                        (projectOperationIds == null || projectOperationIds.Contains(oo.ProjectOperationDetail.ProjectOperation.Id)) &&
                        (projectOperationDetailIds == null || projectOperationDetailIds.Contains(oo.ProjectOperationDetail.Id)));

        var items = await query.ToListAsync(ct);

        return items;
    }

}
