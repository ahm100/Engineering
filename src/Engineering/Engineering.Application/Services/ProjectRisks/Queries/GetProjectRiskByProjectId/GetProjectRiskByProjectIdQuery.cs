using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;

namespace Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskByProjectId;

public record GetProjectRiskByProjectIdQuery(
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<GetProjectRiskByProjectIdResponse?>;