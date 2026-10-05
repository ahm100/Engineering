namespace Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;

public record UpdateOperationInfoGroupResponse(
    long Id,
    string OperationInfoGroupName,
    string OperationInfoGroupCode
    );
