namespace Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;

public record UpdateOperationInfoGroupRequest(
    long Id,
    string OperationInfoGroupName,
    string OperationInfoGroupCode,
    bool IsActive
     ) : IHttpRequest;
