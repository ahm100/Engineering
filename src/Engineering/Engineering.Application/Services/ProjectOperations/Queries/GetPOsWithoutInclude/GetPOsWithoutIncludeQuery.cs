using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetPOsWithoutInclude;

public record GetPOsWithoutIncludeQuery(
    List<long> Ids
    ) : IQuery<List<ProjectOperation>>;