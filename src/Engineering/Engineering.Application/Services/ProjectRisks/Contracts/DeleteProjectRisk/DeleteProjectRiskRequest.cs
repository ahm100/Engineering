namespace Engineering.Application.Services.ProjectRisks.Contracts.DeleteProjectRisk;

public record DeleteProjectRiskRequest(
    long Id
    ) : IHttpRequest;