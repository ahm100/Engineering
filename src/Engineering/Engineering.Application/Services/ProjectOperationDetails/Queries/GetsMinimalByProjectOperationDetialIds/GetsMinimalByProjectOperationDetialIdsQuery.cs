using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsMinimalByProjectOperationDetialIds;

public record GetsMinimalByProjectOperationDetialIdsQuery(
    List<long>? Ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;
