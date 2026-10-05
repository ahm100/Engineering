namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;

public record GetsFilteredShippingCostResponse(
    List<GetsFilteredShippingCostResponseModel> Data,
    int RowCount
    );
