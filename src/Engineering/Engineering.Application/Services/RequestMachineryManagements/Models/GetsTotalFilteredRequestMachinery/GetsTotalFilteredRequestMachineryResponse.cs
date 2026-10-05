namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetsTotalFilteredRequestMachinery;

public record GetsTotalFilteredRequestMachineryResponse()
{
    public string? HourlyTimes { get; set; }
    public decimal? DailyTimes { get; set; } = 0;
    public decimal? ServiceTimes { get; set; } = 0;
    public decimal? VolumeTimes { get; set; } = 0;
    public decimal? RequestedCount { get; set; } = 0;
    public decimal? FinalPrice { get; set; } = 0;
}
