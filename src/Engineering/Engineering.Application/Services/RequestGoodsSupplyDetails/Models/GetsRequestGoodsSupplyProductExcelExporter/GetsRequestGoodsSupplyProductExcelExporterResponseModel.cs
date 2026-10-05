using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelExporter;

public record GetsRequestGoodsSupplyProductExcelExporterResponseModel
{
    public long? Id { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
    public string? SerialNumber { get; set; }
    public string? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManager { get; set; }
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; }
    public string? ProjectOperationCode { get; set; }
    public long ProjectOperationMeasureId { get; set; }
    public string? MeasurementName { get; set; }
    public decimal? Workload { get; set; }
    public string? ProjectOperationDetailNames { get; set; }
    public string? ProjectOperationDetailCodes { get; set; }
    public string? ProjectOperationDetailDescriptions { get; set; }
    public string? ImportanceDescription { get; set; }
    public string? StatusDescription { get; set; }
    public string? TypeDescription { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductBrand { get; set; }
    public string? ProductBrandModel { get; set; }
    public long? ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public string? ProductGroupCode { get; set; }
    public decimal? RequestedCount { get; set; }
    public decimal? SupplyCount { get; set; }
    public decimal? RemainedCount { get; set; }
    public string? DelivaryDeadLineShamsi { get; set; }
    public string? RequestedDateShamsi { get; set; }
    public string? CreatedShamsi { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? TaxPercentage { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? DiscountByPercentage { get; set; }
    public decimal? DiscountByNumber { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public decimal? TransferPrice { get; set; }
    public decimal? OtherPrice { get; set; }
    public decimal? FinalPrice { get; set; }
    public bool? CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorFullName { get; set; }
    public long? SupplyerId { get; set; }
    public string? SupplyerFullName { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public string? LastDescription { get; set; }
    public long? PackageId { get; set; }
    public string? PackageName { get; set; }
    public int? PackageQuantity { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public string? OperatorAppointmentName { get; set; }
}

public record GetsRequestGoodsSupplyProductManagementExcelExporterModel
{
    public string? RequestNumber { get; set; }
    public long? Id { get; set; }
    public GoodsSupplyManagementType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public GoodsSupplyManagementStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public long? InvoiceId { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? Brand { get; set; }
    public string? BrandModel { get; set; }
    public decimal? RequestedCount { get; set; }
    public decimal? ConfirmedRequestCount { get; set; }
    public long? AlternateId { get; set; }
    public long? OperatorAppointmentId { get; set; }
    public string? OperatorAppointmentName { get; set; }
    public string? Description { get; set; }
    public string? LastDescription { get; set; }
    public string? AssignmentDateShamsi { get; set; }
}
