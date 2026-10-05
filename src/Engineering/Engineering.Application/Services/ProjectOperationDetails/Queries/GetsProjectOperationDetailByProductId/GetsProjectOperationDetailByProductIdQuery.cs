using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProductId;

public record GetsProjectOperationDetailByProductIdQuery(
    long ProjectOperationId,
    long ProductId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;