using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsIncludeLessDetailByProjectOperationId;

public record GetsIncludeLessDetailByProjectOperationIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;