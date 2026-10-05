namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithCostCenter;

public record CreateOperationLocationWithCostCenterRequest(
    long? CostCenterId,
    long? ProjectId,
    string PrivateCode,
    string PrivateName,
    bool IsActive
     ) : IHttpRequest;
