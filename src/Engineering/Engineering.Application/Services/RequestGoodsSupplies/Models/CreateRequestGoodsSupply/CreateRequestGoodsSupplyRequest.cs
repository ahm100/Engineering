using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;

public record CreateRequestGoodsSupplyRequest : IHttpRequest
{
    public long ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public long OperationInfoSeasonId { get; set; }
    public GoodsSupplyType Type { get; set; }
    public long? SupplyerId { get; set; }
    public long? BuyerId { get; set; }
    public long? CurrencyId { get; set; }
    public decimal? TransferPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public decimal? DiscountOnInvoicePercentage { get; set; }
    public decimal? DiscountOnInvoiceNumber { get; set; }
    public decimal? DiscountedPriceOnInvoice { get; set; }
    public decimal? TaxOnInvoicePercentage { get; set; }
    public decimal? TaxOnInvoiceNumber { get; set; }
    public bool? IsPettyCash { get; set; }
    public DateTime? RequestedDate { get; set; }
    public string? Description { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string? ConsumptionAddress { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public PurchaseReason? PurchaseReason { get; set; }
    public required List<CreateRequestGoodsSupplyDetailModel> Details { get; set; }
    public bool IsDraft { get; set; }
}


public record CreateRequestGoodsSupplyValidatorsRequest(
    CreateRequestGoodsSupplyRequest GoodsSupplyRequest
    );
