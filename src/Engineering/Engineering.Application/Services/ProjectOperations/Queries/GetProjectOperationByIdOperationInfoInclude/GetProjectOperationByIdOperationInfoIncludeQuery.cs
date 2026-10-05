using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdOperationInfoInclude;

public record GetProjectOperationByIdOperationInfoIncludeQuery(
    long Id
    ) : IQuery<ProjectOperation>;