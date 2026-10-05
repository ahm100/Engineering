using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;

public record GetFltrProjectRiskRequest(
    List<long>? ProjectIds,
    string? FilterData,
    RiskProbability? RiskProbability,
    RiskImpact? RiskImpact,
    RiskStatus? RiskStatus,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;