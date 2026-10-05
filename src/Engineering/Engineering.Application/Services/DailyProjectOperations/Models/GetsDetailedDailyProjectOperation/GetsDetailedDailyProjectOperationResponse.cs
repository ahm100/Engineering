using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;

public record GetsDetailedDailyProjectOperationResponse(
    List<GetsDetailedDailyProjectOperationModel> Data,
    int RowCount
    );

public record GetsDetailedDailyProjectOperationModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; } = string.Empty;
    public ProjectOperationStatus ProjectOperationStatus { get; set; }
    public string? ProjectOperationStatusDescription => ProjectOperationStatus.GetEnumDescription();
    public DailyProjectOperationType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailCode { get; set; } = string.Empty;
    public decimal ProjectOperationDetailLength { get; set; }
    public decimal ProjectOperationDetailWidth { get; set; }
    public decimal ProjectOperationDetailHeight { get; set; }
    public decimal ProjectOperationDetailWeight { get; set; }
    public decimal ProjectOperationDetailNumber { get; set; }
    public decimal ProjectOperationDetailFinalAmount { get; set; }
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public ProjectOperationDetailStatus ProjectOperationDetailStatus { get; set; }
    public string? ProjectOperationDetailStatusDescription => ProjectOperationDetailStatus.GetEnumDescription();
    public DateTime? ProjectOperationDetailStartDate { get; set; }
    public string? ProjectOperationDetailStartDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailStartDate);
    public DateTime? ProjectOperationDetailEndDate { get; set; }
    public string? ProjectOperationDetailEndDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailEndDate);
    public DateTime? ProjectOperationDetailCreateDate { get; set; }
    public string? ProjectOperationDetailCreateDateShamsi => TimeCalculator.ConvertToShamsi(ProjectOperationDetailCreateDate);
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal DailyProjectOperationFinalAmount => Length * Width * Height * Weight * Number;
    public string? Description { get; set; } = string.Empty;
    public ProjectOperationDetailStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public List<long?> ContractorIds { get; set; } = new List<long?>();
    public string? Contractors { get; set; } = string.Empty;
    public string? ContractorsNickName { get; set; } = string.Empty;
    public string? ServiceInfos { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public bool HaveDocument { get; set; }
    [JsonIgnore]
    public List<decimal>? DeductionAmounts { get; set; } = new List<decimal>();
    public List<DetailedServiceInfoModels>? services { get; set; } = new List<DetailedServiceInfoModels>();
}

public record DetailedServiceInfoModels
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public long ServiceInfoMeasureId { get; set; }
}