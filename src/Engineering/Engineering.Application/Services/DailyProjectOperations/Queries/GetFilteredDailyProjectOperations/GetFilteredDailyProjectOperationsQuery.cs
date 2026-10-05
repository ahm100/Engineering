using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperations;

public record GetFilteredDailyProjectOperationsQuery(
    long CostCenterId,
    long ProjectId,
    long? ContractorId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
