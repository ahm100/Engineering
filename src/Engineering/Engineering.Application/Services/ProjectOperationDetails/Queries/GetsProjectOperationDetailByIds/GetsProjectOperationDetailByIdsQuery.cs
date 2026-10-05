using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIds;

public record GetsProjectOperationDetailByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;