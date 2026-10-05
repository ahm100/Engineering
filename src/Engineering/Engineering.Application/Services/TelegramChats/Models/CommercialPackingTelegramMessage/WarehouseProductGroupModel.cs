namespace Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;

public class WarehouseProductGroupModel
{
    public string? WarehouseName { get; set; }
    public string? CostCenterName { get; set; }
    public List<ProductDetailsModel>? Products { get; set; } = new();
}

public class ProductDetailsModel
{
    public long? RequestNumber { get; set; }
    public string? ProductName { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? RequestCount { get; set; }
    public string? MeasureUnitName { get; set; }
}