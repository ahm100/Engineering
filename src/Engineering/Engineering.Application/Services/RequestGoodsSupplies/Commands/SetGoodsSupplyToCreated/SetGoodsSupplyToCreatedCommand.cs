using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetGoodsSupplyToCreated;

public record SetGoodsSupplyToCreatedCommand(
    RequestGoodsSupply GoodsSupply
    ) : ICommand<RequestGoodsSupply>;
