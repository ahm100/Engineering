using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;

public record RGSupplyDetailsStatusChangerRequest(long RequestGoodsSupplyId,
    GoodsSupplyDetailStatus Status,
    string? Description) : IHttpRequest;