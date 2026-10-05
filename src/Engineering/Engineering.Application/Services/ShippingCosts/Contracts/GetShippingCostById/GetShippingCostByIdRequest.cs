
namespace Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;

public record GetShippingCostByIdRequest(
    long Id
    ) : IHttpRequest;
