using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;

public record RGSStatusChangerRequest(long RequestGoodsSupplyId,
    RGSTypeStatus Status,
    string? Description) : IHttpRequest;