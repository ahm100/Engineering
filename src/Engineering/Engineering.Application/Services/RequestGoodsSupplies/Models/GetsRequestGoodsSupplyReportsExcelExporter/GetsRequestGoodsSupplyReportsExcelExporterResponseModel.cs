using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelExporter;

public record GetsRequestGoodsSupplyReportsExcelExporterResponseModel
{
    public long Id { get; set; }
    public string? RequestNumber { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public GoodsSupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int? RejectedNumber { get; set; } = 0;
    public int? AllInStock { get; set; } = 0;
    public int? InStockNumber { get; set; } = 0;
    public int? AllBetweenStock { get; set; } = 0;
    public int? BetweenStockNumber { get; set; } = 0;
    public int? AllCommerce { get; set; } = 0;
    public int? CommerceNumber { get; set; } = 0;
    public string CreatedOn { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public GoodsSupplyDetailImportance? MaxImportance { get; set; }
    public string? MaxImportanceDescription => MaxImportance is null ? null : MaxImportance.GetEnumDescription();
}

public record GetsRequestGoodsSupplyDetailReportsExcelExporterModel
{
    public long? Id { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
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
    public float? TolerancePercentage { get; set; } = 0;
    public float? ToleranceCount { get; set; } = 0;
    public float? TotalEstimatedCount { get; set; } = 0;
    public float? TotalRequestedCount { get; set; } = 0;
    public float? TotalSupplyCount { get; set; } = 0;
    public float? TotalRemainedCount { get; set; } = 0;
    public float? RequestedCount { get; set; } = 0;
    public float? SupplyCount { get; set; } = 0;
    public float? RemainedCount => RequestedCount - SupplyCount;
    public string? DelivaryDeadLine { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public decimal? TotalPrice { get; set; }
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public GoodsSupplyManagementType? ManagementType { get; set; }
    public string? ManagementTypeDescription => ManagementType?.GetEnumDescription();
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public bool? CheckGroup { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public long? ContractorId { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public long? PackageId { get; set; }
    public decimal? PackageQuantity { get; set; }
    public string? PackageTitle { get; set; }
    public decimal? PackagePackageCount { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public string? LastDescription { get; set; }
}