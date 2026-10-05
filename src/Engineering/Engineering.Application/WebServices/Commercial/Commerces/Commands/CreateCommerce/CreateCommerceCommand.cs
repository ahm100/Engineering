using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCommerce;

public record CreateCommerceCommand(
    long? Id,
    RequestGoodsSupply RequestGoodsSupply,
    RequestGoodsSupplyProduct SupplyProduct,
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    List<long>? ProjectOperationDetailIds,
    decimal RequestCount,
    long? CommerceDestinationWarehouseId,
    List<CreateCommerceRequestRequestDocument>? Documents,
    bool CreateInvoice,
    long? ConfirmUserId,
    string? Description
    ) : ICommand<CreateCommerceResponse?>;
