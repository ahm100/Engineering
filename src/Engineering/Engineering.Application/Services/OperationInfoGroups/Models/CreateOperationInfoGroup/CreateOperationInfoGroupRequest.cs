namespace Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;

public record CreateOperationInfoGroupRequest(
    string OperationInfoGroupCode,
    string OperationInfoGroupName,
    bool IsActive
     ) : IHttpRequest;
