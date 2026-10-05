namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;

public record RequestGoodsSupplyProductStatusChangerResponse(
    bool IsDone,
    string? LastDecription
    );