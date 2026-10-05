namespace Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;

public record GetProjectRiskByIdRequest(
    long Id
    ) : IHttpRequest;