using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsForDailyProjectOperations;

public record GetsForDailyProjectOperationsQuery(
    long ProjectId,
    int? Priority,
    string? FilterData,
    DateTime? StartDate,
    DateTime? EndDate,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;