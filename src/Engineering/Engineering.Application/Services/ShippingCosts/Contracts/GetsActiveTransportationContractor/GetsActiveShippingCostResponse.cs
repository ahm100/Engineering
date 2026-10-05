
namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;

public record GetsActiveShippingCostResponse(
    List<GetsActiveShippingCostResponseModel> Data,
    int RowCount
    );
