namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;

public record GetOperationLocationCodeByCostCenterResponse(
    long Id,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    string OperationLocationInfo,
    string Path,
    string Coding,
    long? ParentId,
    long? CostCenterId,
    string? CostCenterName,
    int Priority,
    bool IsActive
    );
