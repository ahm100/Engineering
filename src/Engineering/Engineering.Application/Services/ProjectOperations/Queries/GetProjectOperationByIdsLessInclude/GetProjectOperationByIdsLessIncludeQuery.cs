using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdsLessInclude;

public record GetProjectOperationByIdsLessIncludeQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ProjectOperation>>>;
