using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsMinimalByProjectOperationIds;

public record GetsMinimalByProjectOperationIdsQuery(
    List<long>? ProjectOperationIds,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;
