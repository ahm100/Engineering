using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelEnum;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelExporter;

public record GetsProjectOperationDailyReportingExcelExporterRequest(
    List<long>? Ids,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    List<ProjectOperationDailyExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;