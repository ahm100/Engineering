
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;

public record GetSuggestedServicePriceRequest(
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
    int PageSize,
    decimal? MaxPrice,
    decimal? MinPrice
    ) : IHttpRequest;
