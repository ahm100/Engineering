namespace Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;

public record GetUnAssignedProjectsRequest(
    string? FilterData,
    long? CityId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;