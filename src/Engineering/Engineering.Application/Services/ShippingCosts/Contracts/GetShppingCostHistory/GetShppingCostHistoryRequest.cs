namespace Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;

public record GetShippingCostHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;