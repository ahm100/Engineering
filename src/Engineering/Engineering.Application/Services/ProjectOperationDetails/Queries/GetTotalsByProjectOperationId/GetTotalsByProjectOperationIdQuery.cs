using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetTotalsByProjectOperationId;

public record GetTotalsByProjectOperationIdQuery(
    long ProjectOperationId
    ) : IQuery<List<ProjectOperationDetail>>;