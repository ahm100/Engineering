using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;

namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;

public record GetsOperationLocationExcelExporterRequest(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    long? ParentId,
    string? FilterData,
    List<OperationLocationExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
