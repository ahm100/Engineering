namespace Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;

public record GetShippingCostHistoryResponse(
    List<GetShippingCostHistoryResponseModel> Data,
    int RowCount
    );
