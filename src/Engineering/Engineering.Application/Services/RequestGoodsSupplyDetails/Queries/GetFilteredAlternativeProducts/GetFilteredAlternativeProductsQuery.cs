using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetFilteredAlternativeProducts;

public record GetFilteredAlternativeProductsQuery(
    List<long>? RequestIds,
    List<long>? ProductIds,
    List<long>? RequestGoodsSupplyDetailIds
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
