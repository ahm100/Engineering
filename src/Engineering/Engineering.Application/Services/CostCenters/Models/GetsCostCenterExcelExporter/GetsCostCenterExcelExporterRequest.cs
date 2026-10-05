using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;

namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;

public record GetsCostCenterExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? CostCenterName,
    string? CostCenterCode,
    long? CostCenterTypeId,
    long? InformedUserId,
    long? AuthorizedRoleId,
    long? AuthorizedUserId,
    long? WarehouseId,
    long? CityId,
    List<CostCenterExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
