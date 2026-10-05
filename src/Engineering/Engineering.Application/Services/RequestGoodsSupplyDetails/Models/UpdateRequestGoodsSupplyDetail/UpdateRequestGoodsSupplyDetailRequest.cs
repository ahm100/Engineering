using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;

public record UpdateRequestGoodsSupplyDetailRequest : IHttpRequest
{
    public long RequestGoodsSupplyDetailId { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public long ProductId { get; set; }
    public long ProductGroupId { get; set; }
    public decimal RequestedCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? DiscountByNumber { get; set; }
    public decimal? DiscountByPercentage { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? TaxPercentage { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? PackingPrice { get; set; }
    public DateTime? DelivaryDeadLine { get; set; }
    public GoodsSupplyDetailImportance Importance { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? Description { get; set; }
    public string? ManagementDescription { get; set; }
    public bool CheckGroup { get; set; }
    public long? ContractorId { get; set; }
    public long? PackageId { get; set; }
    public decimal? PackageCount { get; set; }
    public decimal? PackageUnitPrice { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
}
