using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractRegistration;

public class ContractRegistrationFinancialResponse
{
    public decimal InitialAmount { get; set; }
    public decimal FinalContractAmount { get; set; }
    public long CurrencyId { get; set; }
    public string CurrencyName { get; set; } = string.Empty;
    public string CurrencyIso { get; set; } = string.Empty;
    public string? CurrencySymbol { get; set; }
    public bool HasPrepayment { get; set; }
    public bool IsSubjectToAdjustment { get; set; }
    public decimal? ContractCeilingAmount { get; set; }
    public decimal? AdjustmentLimitValue { get; set; }
    public ContractAdjustmentLimitType? AdjustmentLimitType { get; set; }
    public decimal? PrepaymentPercentage { get; set; }
    public decimal? PrepaymentAmount => HasPrepayment && PrepaymentPercentage.HasValue
        ? Engineering.Domain.Entities.Contracts.ContractFinancialMath.CalculatePercentageAmount(
            InitialAmount, PrepaymentPercentage.Value)
        : null;
    public PrepaymentAmortizationMethod? PrepaymentAmortizationMethod { get; set; }
    public decimal? PrepaymentAmortizationValue { get; set; }
    public int? PrepaymentStartStatusStatementNumber { get; set; }
    public decimal? PrepaymentStartProgressPercentage { get; set; }
}
