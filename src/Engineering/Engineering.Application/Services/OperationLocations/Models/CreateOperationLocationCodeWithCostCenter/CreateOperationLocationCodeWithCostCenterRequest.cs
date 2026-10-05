namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithCostCenter;

public record CreateOperationLocationCodeWithCostCenterRequest(
    long? CostCenterId,
    long? ProjectId
     ) : IHttpRequest;
