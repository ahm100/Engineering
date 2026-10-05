using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Commands.UpdateProjectRisk;

public record UpdateProjectRiskCommand(
    long Id,
    string? Code,
    string? Title,
    long? ProjectId,
    RiskProbability? RiskProbability,
    RiskImpact? RiskImpact,
    RiskStatus? RiskStatus,
    bool IsActive) : ICommand<ProjectRisk?>;