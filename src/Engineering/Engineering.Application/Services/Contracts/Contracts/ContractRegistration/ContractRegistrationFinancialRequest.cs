using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractRegistration;

public record ContractRegistrationFinancialRequest(
    decimal InitialAmount,
    long CurrencyId,
    bool HasPrepayment,
    bool IsSubjectToAdjustment,
    decimal? ContractCeilingAmount,
    decimal? AdjustmentLimitValue,
    ContractAdjustmentLimitType? AdjustmentLimitType,
    decimal? PrepaymentPercentage,
    PrepaymentAmortizationMethod? PrepaymentAmortizationMethod,
    decimal? PrepaymentAmortizationValue,
    int? PrepaymentStartStatusStatementNumber,
    decimal? PrepaymentStartProgressPercentage);
