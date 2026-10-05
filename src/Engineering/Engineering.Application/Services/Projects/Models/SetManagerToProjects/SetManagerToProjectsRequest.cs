using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.SetManagerToProjects;

public record SetManagerToProjectsRequest(
    List<long> ProjectIds,
    List<ProjectStatus>? Statuses,
    long ProjectManagerId
     ) : IHttpRequest;
