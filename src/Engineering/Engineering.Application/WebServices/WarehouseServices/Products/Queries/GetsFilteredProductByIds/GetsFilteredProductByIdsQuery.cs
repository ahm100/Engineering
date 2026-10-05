using ProductModel = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Models.Product;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProductByIds;

public record GetsFilteredProductByIdsQuery(
    List<long> Ids,
    bool IgnoreQuery,
    string? FilerData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProductModel>>>;
