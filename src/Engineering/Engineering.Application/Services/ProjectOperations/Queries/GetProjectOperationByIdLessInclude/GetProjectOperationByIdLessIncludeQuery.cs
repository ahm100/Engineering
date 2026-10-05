using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdLessInclude;

public record GetProjectOperationByIdLessIncludeQuery(
    long Id
    ) : IQuery<ProjectOperation>;