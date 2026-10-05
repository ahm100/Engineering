using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;

public record GetsGoodsSupplyManagmentBySupplyProductIdResponse(
    List<GetsGoodsSupplyManagmentBySupplyProductIdModel> Data
    );

public record GetsGoodsSupplyManagmentBySupplyProductIdModel
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
