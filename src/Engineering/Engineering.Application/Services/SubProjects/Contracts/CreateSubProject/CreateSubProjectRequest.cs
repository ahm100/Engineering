using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Contracts.CreateSubProject;

public record CreateSubProjectRequest(
    long ProjectId,
    string Name,
    SubProjectType Type,
    string? Description,
    long? ManagerId,
    DateTime StartDate,
    DateTime? EndDate) : IHttpRequest;
