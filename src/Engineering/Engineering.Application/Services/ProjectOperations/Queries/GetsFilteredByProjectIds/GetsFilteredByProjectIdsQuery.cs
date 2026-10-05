using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredByProjectIds;

public record GetsFilteredByProjectIdsQuery(
    List<long>? ProjectIds,
    List<long>? NotShowProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;