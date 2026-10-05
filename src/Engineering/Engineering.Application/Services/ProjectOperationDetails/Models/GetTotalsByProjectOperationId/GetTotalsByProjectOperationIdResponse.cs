namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsByProjectOperationId;

public record GetTotalsByProjectOperationIdResponse
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? TotalFinalAmounts { get; set; } = 0;
    public decimal? TotalDeductionAmounts { get; set; } = 0;
    public decimal? DailyFinalAmounts { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}

