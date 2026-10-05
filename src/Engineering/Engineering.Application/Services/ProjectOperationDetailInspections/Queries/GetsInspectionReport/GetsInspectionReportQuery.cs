using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetsInspectionReport;

public record GetsInspectionReportQuery(
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
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GetsInspectionReportModel>>>;
