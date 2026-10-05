
namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProductByIds;

public record GetsFilteredProductByIdsRequest(
    List<long> Ids,
    bool IgnoreQuery,
    string? FilerData,
    int PageIndex,
    int PageSize
    );
