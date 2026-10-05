namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;

public record GetsPriceWeightHistoryResponseModel
{
    public long? Id { get; set; }
    public long? PriceWeightId { get; set; }
    public decimal? UntilWeight { get; set; }
    public bool? IsFixed { get; set; }
    public decimal? Price { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
}
