using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductResponse
{
    public long? Id { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public GoodsSupplyType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public GetRequestGoodsSupplyProductDetail Detail { get; set; } = new();
}
