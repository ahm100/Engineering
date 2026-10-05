using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelEnum;

namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;

public record GetsCostOverExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? CostOverCode,
    string? CostOverName,
    List<CostOverExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;