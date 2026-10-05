using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

public sealed record CreateRGSTypeParameters
{
    public required RequestGoodsSupply RequestGoodsSupply { get; init; }

    public GoodsSupplyDetailImportance Importance { get; init; }
    public DateTime? DelivaryDeadLine { get; init; }

    public long ReferenceId { get; init; }
    public SupplyType Type { get; init; }

    public long? PackageId { get; init; }
    public decimal RequestedCount { get; init; }
    public string? ProjectName { get; init; }
    public string? ProjectCode { get; init; }
    public string? ProjectEnName { get; init; }
    public decimal? UnitPrice { get; init; }
    public decimal? TotalPrice { get; init; }

    public decimal? TaxPercentage { get; init; }
    public decimal? TaxNumber { get; init; }

    public decimal? DiscountByPercentage { get; init; }
    public decimal? DiscountByNumber { get; init; }
    public decimal? DiscountedPrice { get; init; }

    public decimal? TransferPrice { get; init; }
    public decimal? PackingPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public decimal? PackageCount { get; init; }
    public decimal? PackageUnitPrice { get; init; }

    public bool CheckGroup { get; init; }

    public long? ContractorId { get; init; }
    public long? DestinationWarehouseId { get; init; }

    public string? CustomerInvoiceNumber { get; init; }
    public string? Description { get; init; }
    public string? ManagementDescription { get; init; }
    public List<string>? Urls { get; init; }
    public bool IsHistoryAdded { get; init; }
}

public sealed record UpdateRGSTypeParameters
{
    public GoodsSupplyDetailImportance Importance { get; init; }
    public DateTime? DelivaryDeadLine { get; init; }

    public string? ProjectName { get; init; }
    public string? ProjectCode { get; init; }
    public string? ProjectEnName { get; init; }
    public long ReferenceId { get; init; }

    public long? PackageId { get; init; }
    public decimal RequestedCount { get; init; }

    public decimal? UnitPrice { get; init; }
    public decimal? TotalPrice { get; init; }

    public decimal? PackingPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public decimal? PackageCount { get; init; }
    public decimal? PackageUnitPrice { get; init; }
    public List<string>? Urls { get; init; }
    public bool CheckGroup { get; init; }

    public long? ContractorId { get; init; }
    public string? Description { get; init; }
    public string? ManagementDescription { get; init; }
}