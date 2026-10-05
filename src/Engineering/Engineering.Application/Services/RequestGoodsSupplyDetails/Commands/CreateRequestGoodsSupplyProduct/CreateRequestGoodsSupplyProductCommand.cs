using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyProduct;

public record CreateRequestGoodsSupplyProductCommand(
        RequestGoodsSupply RequestGoodsSupply,
        GoodsSupplyDetailImportance Importance,
        DateTime? DelivaryDeadLine,
        long ProductId,
        long ProductGroupId,
        long? PackageId,
        decimal RequestedCount,
        decimal? UnitPrice,
        decimal? TaxPercentage,
        decimal? TaxNumber,
        decimal? DiscountByPercentage,
        decimal? DiscountByNumber,
        decimal? PackingPrice,
        decimal? PackageCount,
        decimal? PackageUnitPrice,
        bool CheckGroup,
        long? ContractorId,
        long? DestinationWarehouseId,
        string? CustomerInvoiceNumber,
        string? Description,
        string? ManagementDescription
    ) : ICommand<RequestGoodsSupplyProduct>;

