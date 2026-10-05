using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelEnum;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelExporter;

public record GetsInspectionReportExcelExporterRequest(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? OperationLocationIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? CreatorIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    List<InspectionReportExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
