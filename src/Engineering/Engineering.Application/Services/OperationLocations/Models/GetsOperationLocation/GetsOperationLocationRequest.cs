namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;

public record GetsOperationLocationRequest(
    string? FilterData,
    string? PrivateName,
    string? PrivateCode,
    List<long>? Ids,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
