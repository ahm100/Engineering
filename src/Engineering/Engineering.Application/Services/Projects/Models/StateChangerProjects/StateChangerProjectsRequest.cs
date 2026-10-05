
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.StateChangerProjects;

public record StateChangerProjectsRequest(
    List<long> Ids,
    List<ProjectStatus>? Statuses,
    bool State
    ) : IHttpRequest;
