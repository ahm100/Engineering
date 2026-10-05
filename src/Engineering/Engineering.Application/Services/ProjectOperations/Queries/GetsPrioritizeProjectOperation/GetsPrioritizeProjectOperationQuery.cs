using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsPrioritizeProjectOperation;

public record GetsPrioritizeProjectOperationQuery(
    string? FilterData,
    long? Id,
    int Priority,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;