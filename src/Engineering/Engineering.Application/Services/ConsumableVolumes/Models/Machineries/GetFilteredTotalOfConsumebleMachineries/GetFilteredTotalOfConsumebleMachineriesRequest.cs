namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetFilteredTotalOfConsumebleMachineries;

public record GetFilteredTotalOfConsumebleMachineriesRequest(
    long MachineryId,
    long ProjectId,
    long CostCenterId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds
     ) : IHttpRequest;
