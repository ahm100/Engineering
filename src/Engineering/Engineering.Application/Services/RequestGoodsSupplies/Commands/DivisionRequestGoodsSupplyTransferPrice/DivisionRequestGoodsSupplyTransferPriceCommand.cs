using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DivisionRequestGoodsSupplyTransferPrice;

public record DivisionRequestGoodsSupplyTransferPriceCommand(
    RequestGoodsSupply Entity
    ) : ICommand<RequestGoodsSupply>;
