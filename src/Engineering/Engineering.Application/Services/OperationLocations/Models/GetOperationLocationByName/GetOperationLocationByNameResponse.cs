namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;

public record GetOperationLocationByNameResponse(
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
    long? ProjectId,
    string? ProjectName,
    int Priority,
    bool IsActive
    );
