namespace Engineering.Application.Services.Projects.Models.GetProjectHistory;

public record GetProjectHistoryRequest(
    long Id,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
