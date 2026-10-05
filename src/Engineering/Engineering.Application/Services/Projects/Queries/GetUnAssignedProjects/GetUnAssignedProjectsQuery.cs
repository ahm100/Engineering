using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;

namespace Engineering.Application.Services.Projects.Queries.GetUnAssignedProjects;

public record GetUnAssignedProjectsQuery(
    string? FilterData,
    long? CityId,
    int PageIndex,
    int PageSize
     ) : IQuery<GetUnAssignedProjectsResponse?>;