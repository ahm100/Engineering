using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateProjectRequestGoodsSuppliesDraft;

public record CreateProjectRequestGoodsSupplyDraftRequest : IHttpRequest
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
    public List<CreateProjectRequestGoodsSupplyDetailModel> Details { get; set; }
}