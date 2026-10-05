using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;

public record GetFltrRGSResponse(
    List<GetFltrRGSModel> Data,
    int RowCount);
public class GetFltrRGSModel
{
    public long Id { get; set; }
    public string? RequestSerialNumber { get; set; }
    public string? SerialNumber { get; set; }
    public GoodsSupplyStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public GoodsSupplyType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public PurchaseLocation? PurchaseLocation { get; set; }
    public string? PurchaseLocationDescription => PurchaseLocation?.GetEnumDescription();
    public PurchaseReason? PurchaseReason { get; set; }
    public string? PurchaseReasonDescription => PurchaseReason?.GetEnumDescription();
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public ServiceReasonType? ServiceReasonType { get; set; }
    public string? ServiceReasonTypeDescription => ServiceReasonType?.GetEnumDescription();
    public long? SupplyerId { get; set; }
    public string? Supplyer { get; set; }
    public long? BuyerId { get; set; }
    public string? Buyer { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public decimal? TransferPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public decimal? DiscountOnInvoicePercentage { get; set; }
    public decimal? DiscountOnInvoiceNumber { get; set; }
    public decimal? DiscountedPriceOnInvoice { get; set; }
    public decimal? TaxOnInvoicePercentage { get; set; }
    public decimal? TaxOnInvoiceNumber { get; set; }
    public decimal? FinalInvoiceAmount { get; set; }
    public DateTime? RequestedDate { get; set; }
    public string? RequestedDateShamsi => RequestedDate.ToShamsi();
    public DateTime? DeliveryDeadline { get; set; }
    public string? DeliveryDeadlineShamsi => DeliveryDeadline.ToShamsi();
    public string? RegistrationNumber { get; set; }
    public long? RequestingOrganizationId { get; set; }
    public string? RequestingOrganization { get; set; }
    public string? DescriptionEn { get; set; }
    public string? Description { get; set; }
    public string? DeviceName { get; set; }
    public string? DeviceEnName { get; set; }
    public string? DeviceNumber { get; set; }
    public string? DeviceCode { get; set; }
    public string? UnitCode { get; set; }
    public bool? IsPettyCash { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string? ConsumptionAddress { get; set; }
    public bool? IsProjectSupply { get; set; }
    public long? ProjectId { get; set; }
    public string? Project { get; set; }
    public long TypeCount { get; set; }
    public long TypeDetailCount { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public List<string>? DocumentIds { get; set; }
}