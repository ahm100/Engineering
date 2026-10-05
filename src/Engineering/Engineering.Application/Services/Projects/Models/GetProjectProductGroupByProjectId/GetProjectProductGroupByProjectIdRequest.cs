namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByProjectId;

public record GetProjectProductGroupByProjectIdRequest(
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;
