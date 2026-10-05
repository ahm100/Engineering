using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationDetailId;

public record GetMachineriesByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumableVolumeMachinery>>>;