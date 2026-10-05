namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;

public record GetsProjectOperationDailyReportingRequest(
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
    : IHttpRequest;