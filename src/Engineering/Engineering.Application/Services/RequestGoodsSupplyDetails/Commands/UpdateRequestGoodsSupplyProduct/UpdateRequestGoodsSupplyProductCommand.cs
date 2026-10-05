using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyProduct;

public record UpdateRequestGoodsSupplyProductCommand(
    RequestGoodsSupplyProduct Entity,
    string? CustomerInvoiceNumber
    ) : ICommand<RequestGoodsSupplyProduct>;
