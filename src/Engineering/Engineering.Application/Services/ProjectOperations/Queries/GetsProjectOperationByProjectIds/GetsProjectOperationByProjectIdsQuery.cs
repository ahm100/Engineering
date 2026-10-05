using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByProjectIds;

public record GetsProjectOperationByProjectIdsQuery(
    List<long>? ProjectIds,
    List<long>? ContractorIds,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;