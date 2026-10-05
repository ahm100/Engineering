using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductByIdModeled;

public record GetRequestGoodsSupplyProductByIdModeledQuery(
    long Id
    ) : IQuery<GetRequestGoodsSupplyProductByIdResponse>;
