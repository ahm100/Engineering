namespace Engineering.Application.Services.TelegramChats.TelegramServices.Models;

public class ProductModel
{
    public string ProductName { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public decimal? RequestedCount { get; set; }
}