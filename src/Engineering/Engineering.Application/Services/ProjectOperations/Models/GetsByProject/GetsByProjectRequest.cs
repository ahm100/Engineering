namespace Engineering.Application.Services.ProjectOperations.Models.GetsByProject;

public record GetsByProjectRequest(
    long ProjectId,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
