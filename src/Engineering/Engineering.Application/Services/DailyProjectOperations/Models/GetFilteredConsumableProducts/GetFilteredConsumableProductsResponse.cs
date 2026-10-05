namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;

public record GetFilteredConsumableProductsResponse(
    List<GetFilteredConsumableProductsModel> Data,
    int RowCount
    );
