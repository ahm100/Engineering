using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Contracts.UpdateSubProject;

public record UpdateSubProjectRequest(
    long Id,
    string Name,
    SubProjectType Type,
    string? Description,
    long? ManagerId,
    DateTime StartDate,
    DateTime? EndDate) : IHttpRequest;
