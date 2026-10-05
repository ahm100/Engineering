using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByIdsIncludeless;

public record GetsProjectOperationDetailByIdsIncludelessQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;