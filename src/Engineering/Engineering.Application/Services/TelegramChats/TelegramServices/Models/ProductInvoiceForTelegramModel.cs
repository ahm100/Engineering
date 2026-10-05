namespace Engineering.Application.Services.TelegramChats.TelegramServices.Models;

public class ProductInvoiceForTelegramModel
{
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? MeasureUnitName { get; set; }
    public string? Status { get; set; }
}