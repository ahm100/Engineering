using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;

public record GetRequestGoodsSupplyProductByIdResponse
{
    public long? Id { get; set; }
    public long? RequestGoodsSupplyId { get; set; }
    public string? SerialNumber { get; set; }
    public string? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManager { get; set; }
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; }
    public string? ProjectOperationCode { get; set; }
    public long ProjectOperationMeasureId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public List<long>? ProjectOperationDetailIds { get; set; }
    public List<string>? GetsProjectOperationDetailName { get; set; }
    public string? ProjectOperationDetailNames => string.Join(" - ", GetsProjectOperationDetailName ?? []);
    public List<string>? GetsProjectOperationDetailCode { get; set; }
    public string? ProjectOperationDetailCodes => string.Join(" - ", GetsProjectOperationDetailCode ?? []);
    public List<string?>? GetsProjectOperationDetailDescription { get; set; }
    public string? ProjectOperationDetailDescriptions => string.Join(" - ", GetsProjectOperationDetailDescription ?? []);
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public GoodsSupplyType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductBrand { get; set; }
    public string? ProductBrandModel { get; set; }
    public long? ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public string? ProductGroupCode { get; set; }
    public string? ProductGroupMeasurementName { get; set; }
    public decimal? RequestedCount { get; set; } = 0;
    public decimal? SupplyCount { get; set; } = 0;
    public decimal? RemainedCount => RequestedCount - SupplyCount;
    public DateTime? DelivaryDeadLine { get; set; }
    public DateTime? RequestedDate { get; set; }
    public DateTime? Created { get; set; }
    public decimal? UnitPrice { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? TaxPercentage { get; set; } = 0;
    public decimal? TaxNumber { get; set; } = 0;
    public decimal? DiscountByPercentage { get; set; } = 0;
    public decimal? DiscountByNumber { get; set; } = 0;
    public decimal? DiscountedPrice { get; set; } = 0;
    public decimal? PackingPrice { get; set; } = 0;
    public decimal? TransferPrice { get; set; } = 0;
    public decimal? OtherPrice { get; set; } = 0;
    public decimal? FinalPrice { get; set; } = 0;
    public bool? CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorFullName { get; set; }
    public long? SupplyerId { get; set; }
    public string? SupplyerFullName { get; set; }
    public long? BuyerId { get; set; }
    public string? BuyerFullName { get; set; }
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
    public decimal? PackageQuantity { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public string? OperatorAppointmentName { get; set; }
    public bool? IsPettyCash { get; set; }
    public DateTime? DeliveryDeadline { get; set; }
    public string? DeliveryDeadlineShamsi => DeliveryDeadline.ToShamsi();
    public string? RegistrationNumber { get; set; }
    public long? RequestingOrganizationId { get; set; }
    public string? RequestingOrganization { get; set; }
    public string? DescriptionEn { get; set; }
    public string? ConsumptionRateAndInventoryUrl { get; set; }
    public string? ConsumptionAddress { get; set; }
    public PurchaseLocation? PurchaseLocation { get; set; }
    public string? PurchaseLocationDescription => PurchaseLocation?.GetEnumDescription();
    public List<string>? Documents { get; set; } = new();
    public List<GetFilteredRequestGoodsSupplyDetailsModel> Details { get; set; } = new();
    public List<GetsRequestGoodsSupplyProductManagement>? Managements { get; set; } = new();
}

public record GetsRequestGoodsSupplyProductManagement
{
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
    public DateTime? AssignmentDate { get; set; }
}
