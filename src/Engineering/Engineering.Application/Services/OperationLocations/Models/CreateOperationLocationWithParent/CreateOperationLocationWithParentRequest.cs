namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithParent;

public record CreateOperationLocationWithParentRequest(
    long ParentId,
    string PrivateCode,
    string PrivateName,
    int Priority,
    bool IsActive
     ) : IHttpRequest;
