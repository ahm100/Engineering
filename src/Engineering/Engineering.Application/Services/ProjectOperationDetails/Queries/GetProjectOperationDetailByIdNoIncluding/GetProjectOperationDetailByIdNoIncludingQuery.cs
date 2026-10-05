using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdNoIncluding;

public record GetProjectOperationDetailByIdNoIncludingQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;