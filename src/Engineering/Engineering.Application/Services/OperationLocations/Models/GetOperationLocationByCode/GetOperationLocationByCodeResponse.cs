namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;

public record GetOperationLocationByCodeResponse(
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
    long? ProjectId,
    string? ProjectName,
    int Priority,
    bool IsActive
    );
