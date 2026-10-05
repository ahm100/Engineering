using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;

public record GetProjectOperationDetailByIdLessIncludeQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;