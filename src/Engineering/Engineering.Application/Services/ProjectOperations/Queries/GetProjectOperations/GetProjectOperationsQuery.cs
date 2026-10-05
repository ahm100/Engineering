using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperations;

public record GetProjectOperationsQuery(
    List<long> Ids
    ) : IQuery<List<ProjectOperation>>;