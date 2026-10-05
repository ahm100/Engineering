using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetByIdWithDependencies;

public record GetByIdWithDependenciesQuery(
    long Id
    ) : IQuery<ProjectOperation>;