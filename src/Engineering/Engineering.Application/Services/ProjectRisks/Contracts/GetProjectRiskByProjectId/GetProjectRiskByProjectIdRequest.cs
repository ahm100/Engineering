namespace Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;

public record GetProjectRiskByProjectIdRequest(
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;