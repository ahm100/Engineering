using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessIncludes;

public record GetProjectOperationDetailByIdLessIncludesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;