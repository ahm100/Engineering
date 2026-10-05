using Engineering.Application.Services.Categories.Models.GetsCategoryExcelEnum;

namespace Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;

public record GetsCategoryExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    List<CategoryExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;