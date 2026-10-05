using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractFinancialInformation;

public record UpdateContractFinancialInformationRequest(
    long ContractId,
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
    decimal? PrepaymentStartProgressPercentage) : IHttpRequest;
