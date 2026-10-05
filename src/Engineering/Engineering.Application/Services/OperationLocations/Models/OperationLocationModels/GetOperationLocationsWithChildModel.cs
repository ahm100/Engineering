namespace Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

public record GetOperationLocationsWithChildModel(
    long Id,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    string Path,
    string Coding,
    long? ParentId,
    long? CostCenterId,
    string? CostCenterName,
    long? ProjectId,
    string? ProjectName,
    string OperationLocationInfo,
    int Priority,
    bool IsActive,
    bool HaveChild,
    int ChildCount,
    long? CompanyId,
    string? CompanyNameFa
    );
