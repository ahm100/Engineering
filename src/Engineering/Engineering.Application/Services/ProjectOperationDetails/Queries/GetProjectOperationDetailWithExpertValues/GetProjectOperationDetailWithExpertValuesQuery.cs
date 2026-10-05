using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithExpertValues;

public record GetProjectOperationDetailWithExpertValuesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;
