namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperations;

public record GetSchedulingOperationInfosRequest(
    long CostCenterId,
    long ProjectId,
    string? FilterData,
    List<long>? OperationInfoIds,
    List<long>? OperationLocationsIds,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
