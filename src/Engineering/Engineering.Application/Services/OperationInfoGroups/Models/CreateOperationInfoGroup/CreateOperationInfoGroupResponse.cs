namespace Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;

public record CreateOperationInfoGroupResponse(
    long Id,
    string OperationInfoGroupCode,
    string OperationInfoGroupName
    );
