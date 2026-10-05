using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;

namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelExporter;

public record GetsShippingCostExcelExporterRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    List<long>? ThirdpartyIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize,
    List<ShippingCostExcelEnum>? ExcelFilters
     ) : IHttpRequest;
