using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSStatusChanger;

public record RGSStatusChangerCommand(long RequestGoodsSupplyId,
    RGSTypeStatus Status,
    string? Description) : ICommand<RGSStatusChangerResponse?>;