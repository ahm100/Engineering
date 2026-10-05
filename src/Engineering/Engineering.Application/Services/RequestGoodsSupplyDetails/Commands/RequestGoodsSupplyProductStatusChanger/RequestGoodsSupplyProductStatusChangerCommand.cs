using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.RequestGoodsSupplyProductStatusChanger;

public record RequestGoodsSupplyProductStatusChangerCommand(
    RequestGoodsSupplyProduct Entity,
    GoodsSupplyDetailStatus Status,
    bool? SendToSupply,
    List<long>? RejectedDetailIds,
    List<long>? ConfirmedDetailIds,
    string? LastDescription,
    bool HasGoodsManager
    ) : ICommand<RequestGoodsSupplyProduct>;

