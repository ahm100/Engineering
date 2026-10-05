using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForUpdate;

public record GetProjectOperationForUpdateQuery(
    long Id
    ) : IQuery<ProjectOperation>;