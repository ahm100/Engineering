using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailById;

public record GetProjectOperationDetailByIdQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;