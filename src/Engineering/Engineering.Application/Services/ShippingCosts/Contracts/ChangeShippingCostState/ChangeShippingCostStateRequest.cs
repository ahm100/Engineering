namespace Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;

public record ChangeShippingCostStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
