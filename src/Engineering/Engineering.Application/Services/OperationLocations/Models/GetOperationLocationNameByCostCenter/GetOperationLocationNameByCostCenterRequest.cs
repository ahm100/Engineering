namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;

public record GetOperationLocationNameByCostCenterRequest(
    string PrivateName,
    long? CostCenterId,
    long? ProjectId
     ) : IHttpRequest;
