using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;

namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;

public record GetsOperationInfoExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    long? DependencyId,
    bool? IsActive,
    List<OperationInfoExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
