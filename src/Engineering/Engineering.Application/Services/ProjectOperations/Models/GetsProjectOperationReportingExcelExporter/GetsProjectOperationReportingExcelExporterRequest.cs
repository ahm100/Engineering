using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelEnum;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;

public record GetsProjectOperationReportingExcelExporterRequest(
    List<long>? Ids,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ContractorIds,
    List<ProjectOperationStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? MinimumWorkload,
    decimal? MinimumDoneWorkload,
    decimal? MinimumRemaindedWorkload,
    string? Description,
    string? DailyDescription,
    string? FilterData,
    List<ProjectOperationExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
