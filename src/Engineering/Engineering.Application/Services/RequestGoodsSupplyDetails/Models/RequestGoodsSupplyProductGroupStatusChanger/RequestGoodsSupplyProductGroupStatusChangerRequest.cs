using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductGroupStatusChanger;

public record RequestGoodsSupplyProductGroupStatusChangerRequest(
    List<long> Ids,
    GoodsSupplyDetailStatus Status,
    bool? SendToSupply,
    string? Description
    ) : IHttpRequest;
