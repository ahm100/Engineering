using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredProjectOperationsByProjectIds;

public record GetFilteredProjectOperationsByProjectIdsQuery(
    List<long> ProjectIds,
    long CostCenterId,
    long? ContractorId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
