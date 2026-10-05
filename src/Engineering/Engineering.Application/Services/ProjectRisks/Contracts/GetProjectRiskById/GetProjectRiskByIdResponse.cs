using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;

public record GetProjectRiskByIdResponse
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public RiskProbability RiskProbability { get; set; }
    public string RiskProbabilityDescription => RiskProbability.GetEnumDescription();
    public RiskImpact RiskImpact { get; set; }
    public string RiskImpactDescription => RiskImpact.GetEnumDescription();
    public RiskStatus RiskStatus { get; set; }
    public string RiskStatusDescription => RiskStatus.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
}