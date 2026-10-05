using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;

public record GetsDailyProjectOperationServiceResponse(
    GetsDailyProjectOperationServiceTotalModel OtherData,
    List<GetsDailyProjectOperationServiceModel> Data,
    int RowCount
    );

public record GetsDailyProjectOperationServiceModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; } = string.Empty;
    public ProjectOperationStatus? ProjectOperationStatus { get; set; }
    public string? ProjectOperationStatusDescription => ProjectOperationStatus?.GetEnumDescription();
    public long? UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public decimal? ProjectOperationDetailFinalAmount { get; set; }
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public long? ProjectOperationDetailServiceInfoId { get; set; }
    public string? ProjectOperationDetailServiceInfoName { get; set; } = string.Empty;
    public long? ProjectOperationDetailServiceInfoMeasureId { get; set; }
    public string? ProjectOperationDetailServiceInfoMeasure { get; set; } = string.Empty;
    public decimal? ProjectOperationDetailVolume { get; set; }
    public long? ProjectOperationDetailContractorId { get; set; }
    public string? ProjectOperationDetailContractor { get; set; } = string.Empty;
    public string? ProjectOperationDetailContractorNickName { get; set; } = string.Empty;
    public ProjectOperationDetailStatus? ProjectOperationDetailStatus { get; set; }
    public string? ProjectOperationDetailStatusDescription => ProjectOperationDetailStatus?.GetEnumDescription();
    public DateTime? ProjectOperationDetailStartDate { get; set; }
    public string? ProjectOperationDetailStartDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailStartDate);
    public DateTime? ProjectOperationDetailEndDate { get; set; }
    public string? ProjectOperationDetailEndDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailEndDate);
    public DateTime? ProjectOperationDetailCreateDate { get; set; }
    public string? ProjectOperationDetailCreateDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailCreateDate);
    public long? OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? DailyProjectOperationId { get; set; }
    public decimal? DailyProjectOperationFinalAmount { get; set; }
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public long? ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasure { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public decimal? ProjectServiceVolume { get; set; }
    public string? Description { get; set; } = string.Empty;
    public ProjectOperationDetailStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public DailyProjectOperationType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; }
    public List<decimal>? DeductionAmounts { get; set; } = new List<decimal>();
}

public record GetsDailyProjectOperationServiceTotalModel
{
    public decimal TotalProjectOperationDetailServiceVolume { get; set; } = 0;
    public decimal TotalDailyServiceVolume { get; set; } = 0;
    public decimal RemainingServiceVolume => TotalProjectOperationDetailServiceVolume - TotalDailyServiceVolume;
}