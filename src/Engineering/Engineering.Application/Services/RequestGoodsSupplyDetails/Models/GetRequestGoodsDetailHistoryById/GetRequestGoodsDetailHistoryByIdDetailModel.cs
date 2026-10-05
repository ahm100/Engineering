using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;

public record GetRequestGoodsDetailHistoryByIdDetailModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? Created { get; set; }
    public GoodsSupplyDetailStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public decimal RequestedCount { get; set; }
    public string? DelivaryDeadLine { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public decimal? TaxPercentage { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? DiscountByNumber { get; set; }
    public decimal? DiscountByPercentage { get; set; }
    public decimal? PackingPrice { get; set; }
    public decimal? TransferPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public decimal? FinalPrice { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
}
