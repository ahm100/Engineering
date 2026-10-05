using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;

public record UpdateRGSTypeRequest
(
    long RequestGoodsSupplyId,
    long ProjectId,
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
    string? DeviceName,
    string? DeviceEnName,
    string? DeviceNumber,
    string? DeviceCode,
    string? UnitCode,
    GoodsSupplyDetailImportance? Importance,
    DateTime? RequestedDate,
    List<CreateRGSTypeProductModel>? CreateProductTypes,
    List<UpdateProductRGSTypeModel>? UpdateProductTypes,
    List<long>? DeleteProductTypes,
    List<CreateRGSTypeServiceModel>? CreateServiceTypes,
    List<UpdateServiceRGSTypeModel>? UpdateServiceTypes,
    List<long>? DeleteServiceTypes,
    List<CreateRGSTypeAdvertisementModel>? CreateAdvertisementTypes,
    List<UpdateAdvertisementRGSTypeModel>? UpdateAdvertisementTypes,
    List<long>? DeleteAdvertisementTypes,
    List<CreateRGSTypeProjectModel>? CreateProjectTypes,
    List<UpdateProjectRGSTypeModel>? UpdateProjectTypes,
    List<long>? DeleteProjectTypes,
    bool? IsPettyCash,
    string Description,
    string DescriptionEn,
    DateTime? DeliveryDeadline,
    string? RegistrationNumber,
    long RequestingOrganizationId,
    string? ConsumptionRateAndInventoryUrl,
    string? ConsumptionAddress,
    PurchaseLocation? PurchaseLocation,
    PurchaseReason? PurchaseReason,
    ServiceReasonType? ServiceReasonType,
    bool IsDraft,
    bool IsArchived = false
    ) : IHttpRequest;

public record UpdateProductRGSTypeModel
{
    public long RequestGoodsSupplyTypeId { get; set; }
    public long ReferenceId { get; set; }
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
    public string? CustomerInvoiceNumber { get; set; }
    public List<CreateRGSTypeDetailProductModel>? CreateDetails { get; set; }
    public List<UpdateRGSTypeDetailProductModel>? UpdateDetails { get; set; }
    public List<long>? DeleteIds { get; set; }
}

public record UpdateRGSTypeDetailProductModel
{
    public long RGSTypeDetailId { get; set; }
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

public record UpdateServiceRGSTypeModel
{
    public long RequestGoodsSupplyTypeId { get; set; }
    public long ReferenceId { get; set; }
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
    public string? CustomerInvoiceNumber { get; set; }
    public List<CreateRGSTypeDetailServiceModel>? CreateDetails { get; set; }
    public List<UpdateRGSTypeDetailServiceModel>? UpdateDetails { get; set; }
    public List<long>? DeleteIds { get; set; }
}

public record UpdateRGSTypeDetailServiceModel
{
    public long RGSTypeDetailId { get; set; }
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

public record UpdateAdvertisementRGSTypeModel
{
    public long RequestGoodsSupplyTypeId { get; set; }
    public long ReferenceId { get; set; }
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
    public string? CustomerInvoiceNumber { get; set; }
    public List<CreateRGSTypeDetailAdvertisementModel>? CreateDetails { get; set; }
    public List<UpdateRGSTypeDetailAdvertisementModel>? UpdateDetails { get; set; }
    public List<long>? DeleteIds { get; set; }
}

public record UpdateRGSTypeDetailAdvertisementModel
{
    public long RGSTypeDetailId { get; set; }
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

public record UpdateProjectRGSTypeModel
{
    public long RequestGoodsSupplyTypeId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? DiscountByNumber { get; set; }
    public decimal? DiscountByPercentage { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? TaxPercentage { get; set; }
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
    public string? CustomerInvoiceNumber { get; set; }
    public List<CreateRGSTypeDetailProjectModel>? CreateDetails { get; set; }
    public List<UpdateRGSTypeDetailProjectModel>? UpdateDetails { get; set; }
    public List<long>? DeleteIds { get; set; }
}

public record UpdateRGSTypeDetailProjectModel
{
    public long RGSTypeDetailId { get; set; }
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

public record UpdateRGSTypeDetailModel
{
    public long RGSTypeDetailId { get; set; }
    public SupplyType Type { get; set; }
    public long? ReferenceId { get; set; }
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