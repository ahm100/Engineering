using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithStandards;

public record GetProjectOperationDetailWithStandardsQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;