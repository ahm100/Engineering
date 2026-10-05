using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;

public class GetContractFinancialInformationResponse
{
    public long Id { get; set; }
    public long ContractId { get; set; }

    public decimal InitialAmount { get; set; }

    public long CurrencyId { get; set; }
    public string CurrencyName { get; set; } = string.Empty;
    public string CurrencyIso { get; set; } = string.Empty;
    public string? CurrencySymbol { get; set; }

    public bool HasPrepayment { get; set; }
    public bool IsSubjectToAdjustment { get; set; }

    public decimal? ContractCeilingAmount { get; set; }

    public decimal? AdjustmentLimitValue { get; set; }
    public ContractAdjustmentLimitType? AdjustmentLimitType { get; set; }

    public string? AdjustmentLimitTypeTitle =>
        AdjustmentLimitType?.GetEnumDescription();

    public decimal? PrepaymentPercentage { get; set; }

    public decimal? PrepaymentAmount =>
        HasPrepayment &&
        PrepaymentPercentage.HasValue
            ? Engineering.Domain.Entities.Contracts.ContractFinancialMath
                .CalculatePercentageAmount(
                InitialAmount,
                PrepaymentPercentage.Value)
            : null;

    public PrepaymentAmortizationMethod? PrepaymentAmortizationMethod { get; set; }

    public string? PrepaymentAmortizationMethodTitle =>
        PrepaymentAmortizationMethod?.GetEnumDescription();

    public decimal? PrepaymentAmortizationValue { get; set; }

    public int? PrepaymentStartStatusStatementNumber { get; set; }

    public decimal? PrepaymentStartProgressPercentage { get; set; }

    public decimal FinalContractAmount { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public decimal? RegisteredInitialAmount { get; set; }
}
