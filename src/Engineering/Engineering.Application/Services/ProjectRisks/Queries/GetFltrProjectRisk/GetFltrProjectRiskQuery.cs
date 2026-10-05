using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Queries.GetFltrProjectRisk;

public record GetFltrProjectRiskQuery(
    List<long>? ProjectIds,
    string? FilterData,
    RiskProbability? RiskProbability,
    RiskImpact? RiskImpact,
    RiskStatus? RiskStatus,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFltrProjectRiskResponse?>;