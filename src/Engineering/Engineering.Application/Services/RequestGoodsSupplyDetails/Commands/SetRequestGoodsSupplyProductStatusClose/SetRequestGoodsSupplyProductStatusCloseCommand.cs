using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.SetRequestGoodsSupplyProductStatusClose;

public record SetRequestGoodsSupplyProductStatusCloseCommand(
    RequestGoodsSupplyProduct Entity
    ) : ICommand<RequestGoodsSupplyProduct>;

