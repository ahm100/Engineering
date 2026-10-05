using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRequestGoodsSupply;

public record DeleteRequestGoodsSupplyCommand(
    long RequestGoodsSupplyId
    ) : ICommand<RequestGoodsSupply>;

