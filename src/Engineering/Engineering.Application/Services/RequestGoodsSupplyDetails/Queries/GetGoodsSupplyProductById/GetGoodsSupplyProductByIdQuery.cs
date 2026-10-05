using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductById;

public record GetGoodsSupplyProductByIdQuery(
    long Id
    ) : IQuery<GetGoodsSupplyProductByIdResponse>;

