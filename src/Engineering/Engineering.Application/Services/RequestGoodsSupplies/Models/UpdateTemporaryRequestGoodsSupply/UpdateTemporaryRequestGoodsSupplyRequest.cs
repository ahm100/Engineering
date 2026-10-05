namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateTemporaryRequestGoodsSupply;

public record UpdateTemporaryRequestGoodsSupplyRequest(
    long RequestGoodsSupplyId,
    List<UpdateTemporaryRequestGoodsSupplyDetailModel> Details
    ) : IHttpRequest;
