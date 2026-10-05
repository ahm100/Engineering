using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdIncludelessNew;

public record GetProjectOperationByIdIncludelessNewQuery(
    long Id
    ) : IQuery<ProjectOperation>;