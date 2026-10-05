using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyProduct;

public record DeleteRequestGoodsSupplyProductCommand(
    RequestGoodsSupplyProduct Entity
    ) : ICommand<RequestGoodsSupplyProduct>;

