namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;

public record GetOperationLocationNameByCostCenterResponse(
    long Id,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    string Path,
    string Coding,
    string OperationLocationInfo,
    long? ParentId,
    long? CostCenterId,
    string? CostCenterName,
    int Priority,
    bool IsActive
    );
