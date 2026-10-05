namespace Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;

public record InActiveShippingCostRequest(
    long Id
    ) : IHttpRequest;
