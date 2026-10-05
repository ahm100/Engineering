namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;

public record GetFltrProductsResponse(
    List<GetFltrProductsModel> Data,
    int RowCount);
public class GetFltrProductsModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? TechnicalCode { get; set; } = string.Empty;
    public long GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
}