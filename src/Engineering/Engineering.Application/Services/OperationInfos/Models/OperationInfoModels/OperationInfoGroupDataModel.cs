namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record OperationInfoGroupDataModel(
    long OperationInfoGroupRelationId,
    long Id,
    string GroupName,
    string GroupCode,
    bool IsActive
 );
