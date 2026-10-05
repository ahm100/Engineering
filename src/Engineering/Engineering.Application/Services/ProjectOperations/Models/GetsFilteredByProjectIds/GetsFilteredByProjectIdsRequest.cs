namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProjectIds;

public record GetsFilteredByProjectIdsRequest(
    List<long>? ProjectIds,
    List<long>? NotShowProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
