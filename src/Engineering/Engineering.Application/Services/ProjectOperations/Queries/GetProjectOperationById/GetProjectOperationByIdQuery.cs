using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationById;

public record GetProjectOperationByIdQuery(
    long Id
    ) : IQuery<ProjectOperation>;
