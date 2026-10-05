using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

public sealed record CreateRGSParameters
{
    // References
    public Project? Project { get; init; }
    public ProjectOperation? ProjectOperation { get; init; }
    public ProjectOperationDetail? ProjectOperationDetail { get; init; }
    public OperationInfoSeason? OperationInfoSeason { get; init; }

    // General
    public GoodsSupplyType Type { get; init; }
    public GoodsSupplyStatus Status { get; init; }
    public GoodsSupplyDetailImportance? Importance { get; init; }
    public bool IsProjectSupply { get; init; }

    // Parties
    public long? SupplierId { get; init; }
    public long? BuyerId { get; init; }
    public long? CompanyId { get; init; }
    public long? RequestingOrganizationId { get; init; }

    // Financial
    public long? CurrencyId { get; init; }
    public decimal? TransferPrice { get; init; }
    public decimal? OtherPrice { get; init; }
    public decimal? DiscountOnInvoicePercentage { get; init; }
    public decimal? DiscountOnInvoiceNumber { get; init; }
    public decimal? DiscountedPriceOnInvoice { get; init; }
    public decimal? TaxOnInvoicePercentage { get; init; }
    public decimal? TaxOnInvoiceNumber { get; init; }
    public decimal? FinalInvoiceAmount { get; init; }

    // Dates
    public DateTime? RequestedDate { get; init; }
    public DateTime? DeliveryDeadline { get; init; }

    //Service
    public string? DeviceName { get; set; }
    public string? DeviceEnName { get; set; }
    public string? DeviceNumber { get; set; }
    public string? DeviceCode { get; set; }
    public string? UnitCode { get; set; }

    // Misc
    public bool? IsPettyCash { get; init; }
    public string? Description { get; init; }
    public string? DescriptionEn { get; init; }
    public string? RegistrationNumber { get; init; }
    public string? ConfigCode { get; init; }

    public string? ConsumptionRateAndInventoryUrl { get; init; }
    public string? ConsumptionAddress { get; init; }

    public PurchaseLocation? PurchaseLocation { get; init; }
    public PurchaseReason? PurchaseReason { get; init; }
    public ServiceReasonType? ServiceReasonType { get; init; }
    public RequestGoodsSupply? Parent { get; init; }
}

public sealed record UpdateRGSParameters
{
    // Parties
    public long? SupplierId { get; init; }
    public long? BuyerId { get; init; }

    // Financial
    public long? CurrencyId { get; init; }
    public decimal? TransferPrice { get; init; }
    public decimal? OtherPrice { get; init; }
    public decimal? DiscountOnInvoicePercentage { get; init; }
    public decimal? DiscountOnInvoiceNumber { get; init; }
    public decimal? DiscountedPriceOnInvoice { get; init; }
    public decimal? TaxOnInvoicePercentage { get; init; }
    public decimal? TaxOnInvoiceNumber { get; init; }

    // Dates
    public DateTime? RequestedDate { get; init; }
    public DateTime? DeliveryDeadline { get; init; }
    public GoodsSupplyDetailImportance? Importance { get; init; }

    //Service
    public string? DeviceName { get; set; }
    public string? DeviceEnName { get; set; }
    public string? DeviceNumber { get; set; }
    public string? DeviceCode { get; set; }
    public string? UnitCode { get; set; }

    // General
    public bool? IsPettyCash { get; init; }
    public string? Description { get; init; }
    public string? DescriptionEn { get; init; }
    public string? RegistrationNumber { get; init; }

    public long? RequestingOrganizationId { get; init; }

    public string? ConsumptionRateAndInventoryUrl { get; init; }
    public string? ConsumptionAddress { get; init; }

    public PurchaseLocation? PurchaseLocation { get; init; }
    public PurchaseReason? PurchaseReason { get; init; }
    public ServiceReasonType? ServiceReasonType { get; init; }

    public bool IsDraft { get; init; }
    public RequestGoodsSupply? Parent { get; init; }
    public bool? IsArchived { get; init; } = false;
}