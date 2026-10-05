using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdWithoutInclude;

public record GetProjectOperationDetailByIdWithoutIncludeQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;