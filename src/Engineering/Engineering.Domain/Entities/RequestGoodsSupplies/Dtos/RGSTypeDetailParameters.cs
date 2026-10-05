using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

public record CreateRGSTypeDetailParameters
{
    public RequestGoodsSupply RequestGoodsSupply { get; init; } = default!;

    public ProjectProduct? ProjectProduct { get; init; }
    public RequestGoodsSupplyType RequestGoodsSupplyType { get; init; } = default!;

    public CostCenter? CostCenter { get; init; }

    public long? ReferenceId { get; init; }
    public SupplyType Type { get; init; }

    public GoodsSupplyDetailImportance? Importance { get; init; }

    public decimal RequestedCount { get; init; }

    public decimal? UnitPrice { get; init; }
    public decimal? TotalPrice { get; init; }

    public decimal? PackingPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public DateTime? DelivaryDeadLine { get; init; }

    public List<string>? DocumentUrls { get; init; }

    public string? Description { get; init; }
    public string? DescriptionEn { get; init; }
    public string? ManagementDescription { get; init; }
    public string? LastDescription { get; init; }

    public bool CheckGroup { get; init; }

    public long? ContractorId { get; init; }

    public long? PackageId { get; init; }
    public decimal? PackageCount { get; init; }
    public decimal? PackageUnitPrice { get; init; }
}

public sealed record UpdateRGSTypeDetailParameters
{
    public GoodsSupplyDetailImportance? Importance { get; init; }

    public decimal RequestedCount { get; init; }

    public decimal? UnitPrice { get; init; }
    public decimal? TotalPrice { get; init; }

    public decimal? PackingPrice { get; init; }
    public decimal? FinalPrice { get; init; }

    public DateTime? DelivaryDeadLine { get; init; }

    public List<string>? DocumentUrls { get; init; }

    public string? Description { get; init; }
    public string? DescriptionEn { get; init; }
    public string? ManagementDescription { get; init; }
    public string? LastDescription { get; init; }

    public bool CheckGroup { get; init; }

    public long? ContractorId { get; init; }

    public long? PackageId { get; init; }
    public decimal? PackageCount { get; init; }
    public decimal? PackageUnitPrice { get; init; }
    public CostCenter? CostCenter { get; init; }

}