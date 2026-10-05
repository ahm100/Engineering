namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;

public record GetOperationLocationCodeByCostCenterRequest(
    string PrivateCode,
    long? CostCenterId,
    long? ProjectId
     ) : IHttpRequest;
