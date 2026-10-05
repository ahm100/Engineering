namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;

public record GetsActiveShippingCostRequest(
        List<long>? Ids,
        List<long>? ContractorIds,
        List<long>? MachineTypeIds,
        List<long>? ThirdpartyIds,
        DateTime? FromDate,
        DateTime? ToDate,
        string? FilterData,
        int PageIndex,
        int PageSize
    ) : IHttpRequest;
