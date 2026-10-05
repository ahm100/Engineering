using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsByProjectOperationId;

public record GetsByProjectOperationIdQuery(
    long ProjectId,
    int? Priority,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
