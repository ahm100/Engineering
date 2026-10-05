
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsProjectByProjectManagerId;

public record GetsProjectByProjectManagerIdModel(
    long Id,
    List<ProjectStatus>? Statuses,
    string? ProjectCode,
    string ProjectName,
    bool IsActive
    );
