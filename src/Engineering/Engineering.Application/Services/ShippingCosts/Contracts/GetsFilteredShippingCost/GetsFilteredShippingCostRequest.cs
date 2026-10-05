namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;

public record GetsFilteredShippingCostRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    List<long>? ThirdpartyIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;