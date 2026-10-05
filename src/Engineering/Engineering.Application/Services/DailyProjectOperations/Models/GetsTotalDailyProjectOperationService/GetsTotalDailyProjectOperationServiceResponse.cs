namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;

public record GetsTotalDailyProjectOperationServiceResponse
{
    public decimal TotalProjectOperationDetailServiceVolume { get; set; } = 0;
    public decimal TotalDailyServiceVolume { get; set; } = 0;
    public decimal RemainingServiceVolume => TotalProjectOperationDetailServiceVolume - TotalDailyServiceVolume;
}

public record GetsTotalDailyProjectOperationServiceModel
{
    public long? ProjectOperationDetailId { get; set; }
    public decimal? ProjectOperationDetailVolume { get; set; }
    public decimal? Volume { get; set; }
    public DateTime? Created { get; set; }
}
