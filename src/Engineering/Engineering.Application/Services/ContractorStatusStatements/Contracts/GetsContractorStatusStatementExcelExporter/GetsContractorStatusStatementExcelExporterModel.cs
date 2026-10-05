using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelExporter;

public record GetsContractorStatusStatementExcelExporterModel
{
    public long Id { get; set; }
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? StartDate { get; set; } = string.Empty;
    public string? EndDate { get; set; } = string.Empty;
    public string? StartDateShamsi { get; set; } = string.Empty;
    public string? EndDateShamsi { get; set; } = string.Empty;
    public CSSStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal FinalTotalAmount { get; set; }
    public decimal TotalPercentageDoingJobWell { get; set; }
    public decimal TotalDoingJobWellAmount { get; set; }
    public decimal TotalAdvancePaymentAmount { get; set; }
    public decimal TotalPercentageAdvancePayment { get; set; }
    public decimal TotalDailyLatenessPenalty { get; set; }
    public decimal TotalWorkDonePercent { get; set; }
    public decimal TotalWorkDeliveryPercent { get; set; }
    public decimal TotalWorkCompletionPercent { get; set; }
    public decimal ProductsAmount { get; set; }
    public decimal FinesAmount { get; set; }
    public decimal RewardsAmount { get; set; }
    public decimal ThirdPartiesAmount { get; set; }
    public decimal PaymentedAmount { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ManagmentDescription { get; set; } = string.Empty;
    public bool IsLast { get; set; }
};
