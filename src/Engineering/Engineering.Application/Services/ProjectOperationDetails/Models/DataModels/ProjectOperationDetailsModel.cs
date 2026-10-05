using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailStatus;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

public record ProjectOperationDetailsModel
{
    public long Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectManagerName { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(EndDate);
    public decimal Length { get; set; }
    public bool LengthChangeable { get; set; }
    public decimal Width { get; set; }
    public bool WidthChangeable { get; set; }
    public decimal Height { get; set; }
    public bool HeightChangeable { get; set; }
    public decimal Weight { get; set; }
    public bool WeightChangeable { get; set; }
    public decimal Number { get; set; }
    public bool NumberChangeable { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal Volume { get; set; }
    public GetsProjectOperationDetailStatusModel? StatusModel { get; set; }
    public int Priority { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public long? CreatedProductId { get; set; }
    public string? CreatedProductName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public List<PlannerDataModel?> Planners { get; set; } = new List<PlannerDataModel?>();
    public List<ImplementationAssistantDataModel?> ImplementationAssistants { get; set; } = new List<ImplementationAssistantDataModel?>();
    public List<TechnicalAssistantDataModel?> TechnicalAssistants { get; set; } = new List<TechnicalAssistantDataModel?>();
    public CreatorModel? Creator { get; set; }
    public UpdatorModel? Updator { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? ShamsiCreateDate => TimeCalculator.ConvertToShamsi(CreateDate);
    public DateTime? ModifyDate { get; set; }
    public string? ShamsiModifyDate => TimeCalculator.ConvertToShamsi(ModifyDate);
    public bool HaveSupply { get; set; }
    public bool HaveProduct { get; set; }
    public bool HaveDaily { get; set; }
    public int DailyOperationNumber { get; set; } = 0;
    public int SupplyNumber { get; set; } = 0;
    public decimal UsedFinalAmount { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public string? Contractors { get; set; }
    public string? ContractorNicknames { get; set; }
    public List<long>? PlannerUserIds { get; set; } = new List<long>();
    public List<long>? ImplementationAssistantUserIds { get; set; } = new List<long>();
    public List<long>? TechnicalAssistantUserIds { get; set; } = new List<long>();
    public List<long?> ContractorIds { get; set; } = new List<long?>();
    public List<decimal>? DailyAmounts { get; set; } = new List<decimal>();
    public long? ProjectManagerId { get; set; }
    public long? CreatorId { get; set; }
    public long? UpdaterId { get; set; }
    public decimal? WorkLoad { get; set; }
    public List<decimal>? DeductionAmounts { get; set; } = new List<decimal>();
}


