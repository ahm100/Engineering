using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;

public record CreateRGSTypeRequest : IHttpRequest
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
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public bool? IsPettyCash { get; set; }
    public DateTime? RequestedDate { get; set; }
    public DateTime? DeliveryDeadline { get; set; }
    public string? RegistrationNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public bool IsDraft { get; set; }
    public bool IsAchived { get; set; } = false;
    public long? ParentRGSId { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string ConsumptionAddress { get; set; } = string.Empty;
    public string? DeviceName { get; set; }
    public string? DeviceEnName { get; set; }
    public string? DeviceNumber { get; set; }
    public string? DeviceCode { get; set; }
    public string? UnitCode { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public PurchaseReason? PurchaseReason { get; set; }
    public ServiceReasonType? ServiceReasonType { get; set; }
    public List<CreateRGSTypeProductModel>? ProductModels { get; set; }
    public List<CreateRGSTypeServiceModel>? ServiceModels { get; set; }
    public List<CreateRGSTypeAdvertisementModel>? AdvertisementModels { get; set; }
    public List<CreateRGSTypeProjectModel>? ProjectModels { get; set; }
}

public record CreateRGSTypeProductModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
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
    public List<CreateRGSTypeDetailProductModel>? Details { get; set; }
}

public record CreateRGSTypeDetailProductModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? CostCenterId { get; set; }
}

public record CreateRGSTypeServiceModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public List<CreateRGSTypeDetailServiceModel>? Details { get; set; }
}

public record CreateRGSTypeDetailServiceModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? CostCenterId { get; set; }
}

public record CreateRGSTypeAdvertisementModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public List<CreateRGSTypeDetailAdvertisementModel>? Details { get; set; }
}

public record CreateRGSTypeDetailAdvertisementModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? CostCenterId { get; set; }
}

public record CreateRGSTypeProjectModel
{
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? ProjectEnName { get; set; }
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
    public List<CreateRGSTypeDetailProjectModel>? Details { get; set; }
}

public record CreateRGSTypeDetailProjectModel
{
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? CostCenterId { get; set; }
}

public record CreateRGSTypeDetailDefaultModel
{
    public long ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
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
    public long? CostCenterId { get; set; }
}

public record CreateRGSTypeDetailModel
{
    public long? ReferenceId { get; set; }
    public decimal RequestedCount { get; set; }
    public SupplyType? Type { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? CostCenterId { get; set; }
}