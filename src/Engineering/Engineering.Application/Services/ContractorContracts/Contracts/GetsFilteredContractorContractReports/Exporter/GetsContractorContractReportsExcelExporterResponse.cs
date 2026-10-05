
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;

public record GetsContractorContractReportsExcelExporterResponse(
    FileContentResult File
    );

public record GetsContractorContractReportsExcelExporterModel
{
    public long ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public long Id { get; set; }
    public long ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName { get; set; } = string.Empty;
    public string? ContractorContractTypeCode { get; set; } = string.Empty;
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public decimal? TotalAmount { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public string Created { get; set; } = string.Empty;
};
