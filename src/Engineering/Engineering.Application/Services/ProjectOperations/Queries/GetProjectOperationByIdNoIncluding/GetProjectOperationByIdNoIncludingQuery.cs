using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;

public record GetProjectOperationByIdNoIncludingQuery(
    long Id
    ) : IQuery<ProjectOperation>;