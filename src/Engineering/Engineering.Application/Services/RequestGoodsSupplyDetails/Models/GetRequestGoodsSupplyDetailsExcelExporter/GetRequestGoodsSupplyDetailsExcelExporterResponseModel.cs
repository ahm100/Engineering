using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelExporter;

public record GetRequestGoodsSupplyDetailsExcelExporterResponseModel
{
    public long? Id { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
    public string? RequestNumber { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? ConsumableVolumeProductId { get; set; }
    public decimal? ProductNumber { get; set; }
    public long GroupId { get; set; }
    public string? GroupName { get; set; } = string.Empty;
    public string? GroupCode { get; set; } = string.Empty;
    public string? GroupMeasure { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? Brand { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public string? CostCenter { get; set; } = string.Empty;
    public string? Project { get; set; } = string.Empty;
    public string? ProjectOperation { get; set; } = string.Empty;
    public float? TolerancePercentage { get; set; } = 0;
    public float? ToleranceCount { get; set; } = 0;
    public float? TotalEstimatedCount { get; set; } = 0;
    public float? TotalRequestedCount { get; set; } = 0;
    public float? TotalSupplyCount { get; set; } = 0;
    public float? TotalRemainedCount { get; set; } = 0;
    public float? RequestedCount { get; set; } = 0;
    public float? SupplyCount { get; set; } = 0;
    public float? RemainedCount => RequestedCount - SupplyCount;
    public DateTime? DelivaryDeadLine { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public GoodsSupplyType? RequestType { get; set; }
    public string? RequestTypeDescription => RequestType?.GetEnumDescription();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public bool? CheckGroup { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouse { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? NickName { get; set; } = string.Empty;
    public string? CustomerInvoiceNumber { get; set; }
    public long? PackageId { get; set; }
    public decimal? Quantity { get; set; }
    public bool? IsDefault { get; set; }
    public bool? IsActive { get; set; }
    public string? Title { get; set; }
    public decimal? PackageCount { get; set; }
    public long? SupplierId { get; set; }
    public string? Supplier { get; set; }
    public string? LastDescription { get; set; }
}
