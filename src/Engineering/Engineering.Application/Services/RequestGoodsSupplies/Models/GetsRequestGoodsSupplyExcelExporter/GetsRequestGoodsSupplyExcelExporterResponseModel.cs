namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelExporter;

public record GetsRequestGoodsSupplyExcelExporterResponseModel
{
    public long Id { get; set; }
    public string? RequestNumber { get; set; } = string.Empty;
    public string? TypeDescription { get; set; } = string.Empty;
    public string? MaxImportanceDescription { get; set; } = string.Empty;
    public string? StatusDescription { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
    public string? RequestedDate { get; set; } = string.Empty;
    public string? CreatedOn { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
}

public record GetsRequestGoodsSupplyDetailExcelExporterResponseModel
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
    public decimal? TotalPrice { get; set; }
    public string? ImportanceDescription { get; set; }
    public string? StatusDescription { get; set; }
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouse { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public long? PackageId { get; set; }
    public decimal? PackageQuantity { get; set; }
    public string? PackageTitle { get; set; } = string.Empty;
    public decimal? PackageCount { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public string? LastDescription { get; set; }
}

public record GetsRequestGoodsSupplyManagementExcelExporterResponseModel
{
    public long Id { get; set; }
    public long RequestGoodsSupplyDetailId { get; set; }
    public long? ProductId { get; set; }
    public long? WarehouseId { get; set; }
    public string? Warehouse { get; set; } = string.Empty;
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouse { get; set; } = string.Empty;
    public long? InvoiceId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? ConfirmedRequestCount { get; set; }
    public long? AlternateId { get; set; }
    public string? Description { get; set; }
    public string TypeDescription { get; set; } = string.Empty;
    public string StatusDescription { get; set; } = string.Empty;
    public string? AssignmentDate { get; set; }
    public string? LastDescription { get; set; }
}