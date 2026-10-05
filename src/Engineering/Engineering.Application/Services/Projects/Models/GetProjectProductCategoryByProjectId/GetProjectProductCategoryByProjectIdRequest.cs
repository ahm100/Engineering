namespace Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByProjectId;

public record GetProjectProductCategoryByProjectIdRequest(
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;
