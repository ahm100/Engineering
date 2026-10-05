using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Commands.CreateProjectRisk;

public record CreateProjectRiskCommand(
    string Code,
    string Title,
    long ProjectId,
    RiskProbability RiskProbability,
    RiskImpact RiskImpact,
    RiskStatus RiskStatus,
    bool IsActive) : ICommand<ProjectRisk>;