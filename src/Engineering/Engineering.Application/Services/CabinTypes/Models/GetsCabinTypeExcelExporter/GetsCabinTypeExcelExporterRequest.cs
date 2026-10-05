using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;

namespace Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelExporter;

public record GetsCabinTypeExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    List<CabinTypeExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;