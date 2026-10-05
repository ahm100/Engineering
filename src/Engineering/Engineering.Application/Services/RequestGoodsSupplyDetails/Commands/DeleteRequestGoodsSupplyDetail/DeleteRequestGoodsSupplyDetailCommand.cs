using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyDetail;

public record DeleteRequestGoodsSupplyDetailCommand(
    RequestGoodsSupplyDetail Entity
    ) : ICommand<RequestGoodsSupplyDetail>;
