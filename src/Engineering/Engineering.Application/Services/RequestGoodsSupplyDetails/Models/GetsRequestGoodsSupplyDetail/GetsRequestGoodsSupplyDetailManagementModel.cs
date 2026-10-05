using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailManagementModel
{
    public long? Id { get; set; }
    public long? InvoiceId { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public GetRequestGoodsSupplyManagementProductByIdModel? Product { get; set; } = new();
    public decimal? RequestedCount { get; set; }
    public decimal? ConfirmedRequestCount { get; set; }
    public long? AlternateId { get; set; }
    public GoodsSupplyManagementType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public GoodsSupplyManagementStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public long? OperatorAppointmentId { get; set; }
    public string? OperatorAppointmentName { get; set; }
    public string? Description { get; set; }
    public string? LastDescription { get; set; }
    public DateTime? AssignmentDate { get; set; }
}

public record GetRequestGoodsSupplyManagementProductByIdModel
{
    public long? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public string? ProductDescription { get; set; } = string.Empty;
    public string? Brand { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
}