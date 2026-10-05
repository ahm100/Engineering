namespace Engineering.Application.Services.RequestMachineries.Models.GetTotalOnProjectMachineryReports;

public record GetTotalOnProjectMachineryReportsResponse
{
    public decimal? TotalTimeRequired { get; set; } = 0;
    public decimal? TotalRequestedCount { get; set; } = 0;
    public decimal? TotalFinalPrice { get; set; } = 0;
    public decimal? TotalCount { get; set; } = 0;
    public int? TotalDayWork { get; set; }
    public TimeSpan? TotalTimeWork { get; set; }
}
