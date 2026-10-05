using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;

public record GetsByProjectOperationIdExcelExporterResponseModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long ProjectId { get; set; }
    public long CostCenterId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
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
    public ProjectOperationDetailStatus Status { get; set; }
    public string? StatusDescription { get; set; } = string.Empty;
    public int Priority { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public string? Description { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public long? UpdatorId { get; set; }
    public string? Updator { get; set; } = string.Empty;
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? ShamsiStartDate => TimeCalculator.ConvertToShamsi(StartDate);
    public string? ShamsiEndDate => TimeCalculator.ConvertToShamsi(EndDate);
    public string? ShamsiCreateDate => TimeCalculator.ConvertToShamsi(CreateDate);
    public string? ShamsiModifyDate => TimeCalculator.ConvertToShamsi(ModifyDate);
    public decimal UsedFinalAmount { get; set; }
    public string? Contractors { get; set; }
    public string? ContractorNicknames { get; set; }
    public decimal? WorkLoad { get; set; }
}

public record TotalProjectOperationDetailDataExcelExporterModel
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

public record ImplementationAssistantsExcelExporterModel
{
    public long? ProjectOpertationDetailId { get; set; }
    public long? ImplementationAssistantId { get; set; }
    public string? ImplementationAssistantName { get; set; }
    public string? ImplementationAssistantNickName { get; set; }
}

public record TechnicalAssistantsExcelExporterModel
{
    public long? ProjectOpertationDetailId { get; set; }
    public long? TechnicalAssistantId { get; set; }
    public string? TechnicalAssistantName { get; set; }
    public string? TechnicalAssistantNickName { get; set; }
}

public record PlannerAssistantsExcelExporterModel
{
    public long? ProjectOpertationDetailId { get; set; }
    public long? PlannerAssistantId { get; set; }
    public string? PlannerAssistantName { get; set; }
    public string? PlannerAssistantNickName { get; set; }
}