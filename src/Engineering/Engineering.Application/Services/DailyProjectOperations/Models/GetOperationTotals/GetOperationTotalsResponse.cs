namespace Engineering.Application.Services.DailyProjectOperations.Models.GetOperationTotals;

public record GetOperationTotalsResponse
{
    public decimal? ProjectOperationDetailTotalLengths { get; set; } = 0;
    public decimal? ProjectOperationDetailTotalWidths { get; set; } = 0;
    public decimal? ProjectOperationDetailTotalHeights { get; set; } = 0;
    public decimal? ProjectOperationDetailTotalWeights { get; set; } = 0;
    public decimal? ProjectOperationDetailTotalNumbers { get; set; } = 0;
    public decimal? ProjectOperationDetailTotalAmounts { get; set; } = 0;
    public decimal? DailyTotalLengths { get; set; } = 0;
    public decimal? DailyTotalWidths { get; set; } = 0;
    public decimal? DailyTotalHeights { get; set; } = 0;
    public decimal? DailyTotalWeights { get; set; } = 0;
    public decimal? DailyTotalNumbers { get; set; } = 0;
    public decimal? DailyTotalAmounts { get; set; } = 0;
    public decimal? TotalDeductionFinalAmount { get; set; } = 0;
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? TotalOperationAmounts { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
    public decimal? TotalOperationWork { get; set; } = 0;
}

