using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRequestGoodsSupply;

public record UpdateRequestGoodsSupplyCommand(
    RequestGoodsSupply GoodsSupply,
    OperationInfoSeason? OperationInfoSeason,
    long? SupplyerId,
    long? BuyerId,
    long? CurrencyId,
    decimal? TransferPrice,
    decimal? OtherPrice,
    decimal? DiscountOnInvoicePercentage,
    decimal? DiscountOnInvoiceNumber,
    decimal? DiscountedPriceOnInvoice,
    decimal? TaxOnInvoicePercentage,
    decimal? TaxOnInvoiceNumber,
    decimal? FinalInvoiceAmount,
    bool? IsPettyCash,
    string? Description,
    DateTime? RequestedDate,
    string? ConsumptionRateAndInventoryUrl,
    string? ConsumptionAddress,
    PurchaseLocation? PurchaseLocation,
    PurchaseReason? PurchaseReason,
    bool IsDraft
    ) : ICommand<RequestGoodsSupply>;
