using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Contracts.CreateProjectRisk;

public record CreateProjectRiskRequest(
    string Code,
    string Title,
    long ProjectId,
    RiskProbability RiskProbability,
    RiskImpact RiskImpact,
    RiskStatus RiskStatus,
    bool IsActive) : IHttpRequest;