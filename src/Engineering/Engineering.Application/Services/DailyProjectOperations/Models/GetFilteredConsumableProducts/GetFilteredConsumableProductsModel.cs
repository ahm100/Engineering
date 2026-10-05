namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;

public record GetFilteredConsumableProductsModel
{
    public long ConsumableVolumeProductId { get; set; }
    public long ProductGroupId { get; set; }
    public decimal FinalValue { get; set; }
    public string? ProductGroupName { get; set; } = string.Empty;
    public string? ProductGroupCode { get; set; } = string.Empty;
    public List<GetFilteredConsumableProductsDetailModel>? Products { get; set; } = new();
}
