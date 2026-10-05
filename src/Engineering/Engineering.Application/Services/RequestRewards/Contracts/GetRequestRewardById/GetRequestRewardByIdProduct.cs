namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProduct
{
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public long? Id { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public decimal? Price { get; set; }
    public int? Count { get; set; }

}
