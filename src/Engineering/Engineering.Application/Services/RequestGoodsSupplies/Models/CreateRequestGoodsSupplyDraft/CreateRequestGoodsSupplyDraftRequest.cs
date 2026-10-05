using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupplyDraft;

public record CreateRequestGoodsSupplyDraftRequest(
    long ProjectOperationId,
    long? ProjectOperationDetailId,
    long OperationInfoSeasonId,
    GoodsSupplyType Type,
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
    bool? IsPettyCash,
    DateTime? RequestedDate,
    string? Description,
    List<CreateRequestGoodsSupplyDetailModel> Details
    ) : IHttpRequest;
