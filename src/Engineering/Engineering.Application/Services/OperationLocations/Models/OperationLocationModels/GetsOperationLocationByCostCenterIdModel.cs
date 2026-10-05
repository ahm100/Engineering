namespace Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

public record GetsOperationLocationByCostCenterIdModel(
    long Id,
    long? CostCenterId,
    string? CostCenterName,
    long? ProjectId,
    string? ProjectName,
    long? ParentId,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    string OperationLocationInfo,
    string Path,
    string Coding,
    int Priority,
    bool IsActive,
    bool HaveChild,
    int ChildCount
    );
