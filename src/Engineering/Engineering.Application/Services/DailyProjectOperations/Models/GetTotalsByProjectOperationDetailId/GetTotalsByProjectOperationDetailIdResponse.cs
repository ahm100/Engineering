namespace Engineering.Application.Services.DailyProjectOperations.Models.GetTotalsByProjectOperationDetailId;

public record GetTotalsByProjectOperationDetailIdResponse
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? ProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? TotalDeductionFinalAmount { get; set; } = 0;
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}

