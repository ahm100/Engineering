using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;

namespace Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskById;

public record GetProjectRiskByIdQuery(
    long Id
    ) : IQuery<GetProjectRiskByIdResponse?>;