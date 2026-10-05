namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsPdfGoodsSupplyProduct;

public record GetsPdfGoodsSupplyProductResponse(
    List<GetsPdfGoodsSupplyProductModel> Data,
    int RowCount
    );

public record GetsPdfGoodsSupplyProductModel
{
    public string? Number { get; set; } = string.Empty;
    public string? GroupNumber { get; set; } = string.Empty;
    public string? RequestNumber { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? Measure { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? RequestedCount { get; set; } = string.Empty;
    public string? Created { get; set; } = string.Empty;
    public string? Creator { get; set; } = string.Empty;
}
