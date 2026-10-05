using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyDetail;

public record DeleteRequestGoodsSupplyDetailRequest(
    long? RequestGoodsSupplyDetailId
    ) : IHttpRequest;

public record DeleteRequestGoodsSupplyDetailModelRequest(
    long? RequestGoodsSupplyDetailId,
    RequestGoodsSupplyDetail? RequestGoodsSupplyDetail,
    int? AnotherDataCount,
    bool CheckAnotherData,
    bool? HaveAnyForAdd
    );
