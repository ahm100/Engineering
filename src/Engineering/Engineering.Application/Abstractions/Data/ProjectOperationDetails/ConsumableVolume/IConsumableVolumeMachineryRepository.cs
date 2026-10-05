using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;

public interface IConsumableVolumeMachineryRepository : IBaseRepository<ConsumableVolumeMachinery>
{
    Task<ConsumableVolumeMachinery?> GetById(long id, CT ct);

    Task<(List<ConsumableVolumeMachinery> Data, int RowCount)> GetsByProjectOperationDetailId(long id, string? filterData, int pageIndex, int pageSize, CT ct);

    Task<(List<ConsumableVolumeMachinery> Data, int RowCount)> GetsFilteredMachineriyVolume(long projectId, long? projectOperationId, long? projectOperationDetailId, long? machineryGroupId, long? machineryId,
        string? filterData, CT ct);

    Task<(List<MachineriesDataModel> Data, int RowCount)> GetsByProjectOperationId(long id, int pageIndex, int pageSize, CT ct);

    Task<List<ConsumableVolumeMachinery>> GetFilteredTotalOfConsumebleMachineries(
        long machineryId,
        long projectId,
        long costCenterId,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds, CT ct);

}
