namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProducts;

public record GetsFilteredProductsQuery(
    bool IgnoreQuery,
    string? FilerData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<long>?>>;
