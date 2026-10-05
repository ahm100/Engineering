using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyProduct;

public record UpdateRequestGoodsSupplyProductRequest(
    long Id,
    List<CreateRequestGoodsSupplyDetailModel>? Creates,
    List<UpdateRequestGoodsSupplyDetailRequest>? Updates
    ) : IHttpRequest;
