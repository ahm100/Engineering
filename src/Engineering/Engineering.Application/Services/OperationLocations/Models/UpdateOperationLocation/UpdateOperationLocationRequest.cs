
namespace Engineering.Application.Services.OperationLocations.Models.UpdateOperationLocation;

public record UpdateOperationLocationRequest(
    long Id,
    long? ParentId,
    long? ProjectId,
    string PrivateName,
    string PrivateCode,
    int? Priority,
    bool IsActive
     ) : IHttpRequest;
