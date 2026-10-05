using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Exporter;

public record GetSuggestedServicePriceExcelExporterRequest(
    List<long>? Ids,
    List<SuggestedServicePriceEnum>? ExcelFilters,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ContractorIds,
    List<long>? ProjectOperationIds,
    List<long>? ServiceInfoIds,
    string? FilterData,
    DateTime? StartDate,
    DateTime? EndDate,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
) : IHttpRequest;