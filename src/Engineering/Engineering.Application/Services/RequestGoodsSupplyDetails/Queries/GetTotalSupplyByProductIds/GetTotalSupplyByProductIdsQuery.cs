using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProductIds;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProductIds;

public record GetTotalSupplyByProductIdsQuery(
    List<long>? ProductIds
    ) : IQuery<List<GetTotalSupplyByProductIdsModel>>;

