namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationProduct
{
    public long ConsumableVolumeProductId { get; set; }
    public long ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public decimal FinalValue { get; set; }
    public decimal? UnusedValue { get; set; }
    public long ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; } = string.Empty;
}