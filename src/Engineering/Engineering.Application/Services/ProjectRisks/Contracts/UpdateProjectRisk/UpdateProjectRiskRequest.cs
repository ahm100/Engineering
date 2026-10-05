using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Contracts.UpdateProjectRisk;

public record UpdateProjectRiskRequest(
    long Id,
    string? Code,
    string? Title,
    long? ProjectId,
    RiskProbability? RiskProbability,
    RiskImpact? RiskImpact,
    RiskStatus? RiskStatus,
    bool IsActive) : IHttpRequest;