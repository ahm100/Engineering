namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetTotalDailiesByProjectOperationDetailId;

public record GetTotalDailiesByProjectOperationDetailIdResponse
{
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public long? OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public long? OperationLocationId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? ProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? RemainingAmount => ProjectOperationDetailFinalAmount - TotalAmounts;
    public decimal? TotalDeductionFinalAmount { get; set; } = 0;
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}
