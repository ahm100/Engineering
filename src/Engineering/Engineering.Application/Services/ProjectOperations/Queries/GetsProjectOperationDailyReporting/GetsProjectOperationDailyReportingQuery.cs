using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationDailyReporting;

public record GetsProjectOperationDailyReportingQuery(
    List<long>? Ids,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsProjectOperationDailyReportingModel>>>;