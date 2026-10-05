namespace Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithParent;

public record CreateOperationLocationCodeWithParentRequest(
    long ParentId
     ) : IHttpRequest;
