using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetFilteredTotalOfConsumebleMachineries;

public record GetFilteredTotalOfConsumebleMachineriesQuery(
    long MachineryId,
    long ProjectId,
    long CostCenterId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds
    ) : IQuery<List<ConsumableVolumeMachinery>>;
