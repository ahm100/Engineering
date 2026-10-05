using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;

public record RequestGoodsSupplyProductStatusChangerRequest(
    long Id,
    GoodsSupplyDetailStatus Status,
    bool? SendToSupply,
    string? Description
    ) : IHttpRequest;

public record RequestGoodsSupplyProductStatusChangerModelRequest(
    long? Id,
    RequestGoodsSupplyProduct? RequestGoodsSupplyProduct,
    GoodsSupplyDetailStatus Status,
    bool? SendToSupply,
    string? Description
    );