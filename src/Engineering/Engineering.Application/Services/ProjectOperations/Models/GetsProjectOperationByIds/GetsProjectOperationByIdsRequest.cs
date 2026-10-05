namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByIds;

public record GetsProjectOperationByIdsRequest(
    List<long>? Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
