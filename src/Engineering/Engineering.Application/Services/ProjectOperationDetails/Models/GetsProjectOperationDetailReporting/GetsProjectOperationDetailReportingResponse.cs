using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;

public record GetsProjectOperationDetailReportingResponse(
    List<GetsProjectOperationDetailReportingModel> Data,
    int RowCount
    );

public record GetsProjectOperationDetailReportingModel
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ProjectOperationId { get; set; }
    public decimal? ProjectOperationWorkLoad { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; }
    public string? PrivateCode { get; set; }
    public string? PublicName { get; set; }
    public string? PublicCode { get; set; }
    public string? Contractors { get; set; } = string.Empty;
    public string? ContractorsNickName { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal DoneFinalAmount { get; set; }
    public decimal RemaindedFinalAmount => FinalAmount - DoneFinalAmount;
    public ProjectOperationDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(EndDate);
    public int? Day { get; set; }
    public int? Hour { get; set; }
    public int Priority { get; set; }
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? ShamsiCreated => TimeCalculator.ConvertToShamsi(Created);
    public long? UpdaterId { get; set; }
    public string? UpdaterName { get; set; } = string.Empty;
    public string? UpdaterNickname { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? ShamsiUpdated => TimeCalculator.ConvertToShamsi(Updated);
    public string? Description { get; set; } = string.Empty;
    public List<long?> ContractorIds { get; set; } = new List<long?>();
    public List<decimal>? DailyAmounts { get; set; } = new List<decimal>();
    public List<decimal>? DeductionAmounts { get; set; } = new List<decimal>();
}
