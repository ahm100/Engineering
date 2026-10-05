namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;

public record GetsInspectionReportResponse(
    List<GetsInspectionReportModel> Data,
    int RowCount);

public record GetsInspectionReportModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public long? OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public decimal? Workload { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailCode { get; set; }
    public long? OperationLocationId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public decimal? DetailLength { get; set; }
    public decimal? DetailWidth { get; set; }
    public decimal? DetailHeight { get; set; }
    public decimal? DetailWeight { get; set; }
    public decimal? DetailNumber { get; set; }
    public decimal? ProjectOperationDetailFinalValue => DetailFinalAmount - DeductionFinalAmount;
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Number { get; set; }
    public decimal? FinalAmount => (Length ?? 0) * (Width ?? 0) * (Height ?? 0) * (Weight ?? 0) * (Number ?? 0);
    public DateTime? InspectionDate { get; set; }
    public string? InspectionDateShamsi => TimeCalculator.ConvertToShamsi(InspectionDate);
    public string? DetailDescription { get; set; }
    public string? Description { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? CreateDate { get; set; }
    public string? CreateDateShamsi => TimeCalculator.ConvertToShamsi(CreateDate);
    public bool HaveDocument { get; set; }
    public List<InspectionDocumentResponseModel>? Documents { get; set; }
    [JsonIgnore]
    public List<InspectionDetailDeductionResponseModel>? Deductions { get; set; }
    [JsonIgnore]
    public decimal? DetailFinalAmount => (DetailLength ?? 0) * (DetailWidth ?? 0) * (DetailHeight ?? 0) * (DetailWeight ?? 0) * (DetailNumber ?? 0);
    [JsonIgnore]
    public decimal? DeductionFinalAmount => (Deductions?.Sum(x => x.FinalAmount) ?? 0);
}

public record InspectionDocumentResponseModel
{
    public long? Id { get; set; }
    public string? Url { get; set; }
}

public record InspectionDetailDeductionResponseModel
{
    public long Id { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Number { get; set; }
    public decimal? FinalAmount => (Length ?? 0) * (Width ?? 0) * (Height ?? 0) * (Weight ?? 0) * (Number ?? 0);
}