
namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProducts;

public record GetsFilteredProductsRequest(
    bool IgnoreQuery,
    string? FilerData,
    int PageIndex,
    int PageSize
    );
