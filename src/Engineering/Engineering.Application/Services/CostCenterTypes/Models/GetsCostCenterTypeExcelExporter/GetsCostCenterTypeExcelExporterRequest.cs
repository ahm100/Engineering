using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;

namespace Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;

public record GetsCostCenterTypeExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    List<CostCenterTypeExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize)
    : IHttpRequest;