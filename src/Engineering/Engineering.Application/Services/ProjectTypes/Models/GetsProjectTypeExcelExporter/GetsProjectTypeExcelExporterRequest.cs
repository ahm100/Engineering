using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;

namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;

public record GetsProjectTypeExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    List<ProjectTypeExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
