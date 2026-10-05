namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;

public record GetFilteredConsumableProductsDetailModel
{
    public long? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? MeasureUnitName { get; set; } = string.Empty;
    public string? Brand { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
}
