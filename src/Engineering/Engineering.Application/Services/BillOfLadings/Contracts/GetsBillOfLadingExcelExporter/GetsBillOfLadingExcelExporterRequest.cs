using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;

namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;

public record GetsBillOfLadingExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? BillOfLadingCode,
    string? BillOfLadingName,
    string[]? OrderBy,
    List<BillOfLadingExcelEnum>? ExcelFilters,
    bool? IsActive,
    int PageIndex,
    int PageSize)
    : IHttpRequest;