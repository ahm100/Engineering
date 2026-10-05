using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;

namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;

public record GetsServiceInfoExcelExporterRequest(
    List<long>? Ids,
    long? OperationInfoId,
    string? FilterData,
    string? ServiceInfoCode,
    string? ServiceInfoName,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    bool? IsActive,
    List<ServiceInfoExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
