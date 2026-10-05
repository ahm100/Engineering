using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;

public record CreateProjectRequestGoodsSuppliesRequest : IHttpRequest
{
    public long ProjectId { get; set; }
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
    public bool IsDraft { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string? ConsumptionAddress { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public PurchaseReason? PurchaseReason { get; set; }
    public List<CreateProjectRequestGoodsSupplyDetailModel>? Details { get; set; }
}

public record CreateProjectRequestGoodsSupplyDetailModel
{
    public long ProjectId { get; set; }
    public long ProductId { get; set; }
    public long ProductGroupId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? DiscountByNumber { get; set; }
    public decimal? DiscountByPercentage { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? TaxPercentage { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
}

public record CreateProjectRequestGoodsSupplyDetailModelRequest(
    long? RequestGoodsSupplyId,
    RequestGoodsSupply? RequestGoodsSupply,
    List<CreateProjectRequestGoodsSupplyDetailModel> Details
    );
