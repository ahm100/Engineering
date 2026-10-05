using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplySummary;

public class GetRequestGoodsSupplySummaryResponse
{
    public long Id { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public GoodsSupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public GoodsSupplyStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime CreatedOn { get; set; }
    public DateTime? RequestedDate { get; set; }
    public List<GetRequestGoodsSupplySummaryDetailModel> Details { get; set; } = new();
}

public record GetRequestGoodsSupplySummaryDetailModel
{
    public long? Id { get; set; }
    public string? ProductName { get; set; }
    public string? ProductGroup { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public float? RequestedCount { get; set; } = 0;
    public float? SupplyCount { get; set; } = 0;
    public float? RemainedCount => RequestedCount - SupplyCount;
    public GoodsSupplyDetailImportance? Importance { get; set; }
    public string? ImportanceDescription => Importance?.GetEnumDescription();
    public GoodsSupplyDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public string? LastDescription { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorFullName { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public List<GetRequestGoodsSupplySummaryManagementModel>? Managements { get; set; } = new();
}

public record GetRequestGoodsSupplySummaryManagementModel
{
    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public decimal? RequestedCount { get; set; }
    public decimal? ConfirmedRequestCount { get; set; }
    public GoodsSupplyManagementType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public GoodsSupplyManagementStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public string? Description { get; set; }
    public string? LastDescription { get; set; }
    public DateTime? AssignmentDate { get; set; }
}
