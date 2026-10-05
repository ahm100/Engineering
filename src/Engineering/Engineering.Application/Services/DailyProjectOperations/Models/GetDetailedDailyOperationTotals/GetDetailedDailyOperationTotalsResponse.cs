namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;

public record GetDetailedDailyOperationTotalsResponse
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

public record GetDetailedDailyProjectOperationTotalsModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public decimal ProjectOperationWorkload { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public decimal ProjectOperationDetailLength { get; set; }
    public decimal ProjectOperationDetailWidth { get; set; }
    public decimal ProjectOperationDetailHeight { get; set; }
    public decimal ProjectOperationDetailWeight { get; set; }
    public decimal ProjectOperationDetailNumber { get; set; }
    public decimal ProjectOperationDetailFinalAmount { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal DailyProjectOperationFinalAmount => Length * Width * Height * Weight * Number;
    public List<decimal>? DeductionAmounts { get; set; } = new List<decimal>();
}