namespace Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;

public record ActiveShippingCostRequest(
    long Id
    ) : IHttpRequest;
