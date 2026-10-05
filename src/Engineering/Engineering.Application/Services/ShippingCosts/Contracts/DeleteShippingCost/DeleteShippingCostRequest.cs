namespace Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;

public record DeleteShippingCostRequest(
    List<long> Ids
    ) : IHttpRequest;
