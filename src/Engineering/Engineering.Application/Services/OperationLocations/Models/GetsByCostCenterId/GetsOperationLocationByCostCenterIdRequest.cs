namespace Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;

public record GetsOperationLocationByCostCenterIdRequest(
    long? CostCenterId,
    long? ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
