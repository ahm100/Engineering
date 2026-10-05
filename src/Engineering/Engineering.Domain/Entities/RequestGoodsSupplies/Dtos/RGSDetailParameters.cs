using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

public record CreateRGSDetailParameters
{
    public RequestGoodsSupply RequestGoodsSupply { get; init; } = default!;

    public ConsumableVolumeProduct? ConsumableVolumeProduct { get; init; }
    public ProjectProduct? ProjectProduct { get; init; }
    public RequestGoodsSupplyProduct? RequestGoodsSupplyProduct { get; init; }

    public CostCenter? CostCenter { get; init; }

    public long ProductId { get; init; }

    public GoodsSupplyDetailImportance? Importance { get; init; }

    public decimal RequestedCount { get; init; }

    public decimal? UnitPrice { get; init; }
    public decimal? TotalPrice { get; init; }

    public decimal? DiscountByNumber { get; init; }
    public decimal? DiscountByPercentage { get; init; }
    public decimal? DiscountedPrice { get; init; }

    public decimal? TaxNumber { get; init; }
    public decimal? TaxPercentage { get; init; }

    public decimal? PackingPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public DateTime? DelivaryDeadLine { get; init; }

    public List<string>? DocumentUrls { get; init; }

    public string? Description { get; init; }
    public string? ManagementDescription { get; init; }
    public string? LastDescription { get; init; }

    public bool CheckGroup { get; init; }

    public long? ContractorId { get; init; }

    public long? PackageId { get; init; }
    public decimal? PackageCount { get; init; }
    public decimal? PackageUnitPrice { get; init; }

    public long? DestinationWarehouseId { get; init; }

    public string? CustomerInvoiceNumber { get; init; }
}

public sealed record UpdateRGSDetailParameters
    : CreateRGSDetailParameters
{
    public required RequestGoodsSupplyProduct SupplyProduct { get; init; }
}