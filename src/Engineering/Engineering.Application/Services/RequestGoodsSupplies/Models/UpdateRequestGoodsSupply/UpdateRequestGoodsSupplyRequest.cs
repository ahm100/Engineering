using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;

public record UpdateRequestGoodsSupplyRequest(
    long RequestGoodsSupplyId,
    long OperationInfoSeasonId,
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
    DateTime? RequestedDate,
    List<UpdateRequestGoodsSupplyDetailModel> Details,
    bool? IsPettyCash,
    string? Description,
    string? ConsumptionRateAndInventoryUrl,
    string? ConsumptionAddress,
    PurchaseLocation? PurchaseLocation,
    PurchaseReason? PurchaseReason,
    bool IsDraft
    ) : IHttpRequest;
