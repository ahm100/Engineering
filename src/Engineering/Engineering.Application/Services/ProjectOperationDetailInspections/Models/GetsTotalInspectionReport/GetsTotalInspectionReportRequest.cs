namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsTotalInspectionReport;

public record GetsTotalInspectionReportRequest(
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
    string? FilterData) : IHttpRequest;
