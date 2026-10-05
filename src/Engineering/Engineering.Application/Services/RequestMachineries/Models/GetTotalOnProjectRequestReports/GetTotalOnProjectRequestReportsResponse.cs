namespace Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectRequestReports;

public record GetTotalOnProjectRequestReportsResponse
{
    public decimal? TotalTimeRequired { get; set; } = 0;
    public decimal? TotalRequestedCount { get; set; } = 0;
    public decimal? TotalFinalPrice { get; set; } = 0;
    public int? TotalDayWork { get; set; }
    public string? TotalTimeWork { get; set; }
}
