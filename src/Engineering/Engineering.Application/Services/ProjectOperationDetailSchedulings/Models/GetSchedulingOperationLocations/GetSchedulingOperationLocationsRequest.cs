namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetProjectOperationDetails;

public record GetSchedulingOperationLocationsRequest(
    long CostCenterId,
    long ProjectId,
    string? FilterData,
    List<long>? OperationInfoIds,
    List<long>? OperationLocationsIds,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
