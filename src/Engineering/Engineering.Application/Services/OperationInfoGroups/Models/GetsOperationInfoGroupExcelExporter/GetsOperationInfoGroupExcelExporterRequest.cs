using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;

namespace Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;

public record GetsOperationInfoGroupExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    List<OperationInfoGroupExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
