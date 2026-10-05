using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractRegistration;

public class ContractRegistrationFinancialValidator : AbstractValidator<ContractRegistrationFinancialRequest>
{
    public ContractRegistrationFinancialValidator()
    {
        RuleFor(x => x.InitialAmount).IsPositive(ContractCmts.InitialAmount)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.CurrencyId).IsPositive(GlobalCmts.CurrencyId);
        RuleFor(x => x.ContractCeilingAmount).IsOptionalPositive(ContractCmts.ContractCeilingAmount)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.AdjustmentLimitValue).IsOptionalPositive(ContractCmts.AdjustmentLimitValue)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.AdjustmentLimitType).IsNullableEnum(ContractCmts.AdjustmentLimitType);
        RuleFor(x => x.PrepaymentPercentage).IsOptionalPositive(ContractCmts.PrepaymentPercentage)
            .PrecisionScale(ContractFinancialMath.PercentagePrecision, ContractFinancialMath.PercentageScale, true)
            .Must(x => !x.HasValue || x.Value <= 100m).WithError(ContractErrors.ContractFinancialTermsInvalid);
        RuleFor(x => x.PrepaymentAmortizationMethod).IsNullableEnum(ContractCmts.PrepaymentAmortizationMethod);
        RuleFor(x => x.PrepaymentAmortizationValue).IsOptionalPositive(ContractCmts.PrepaymentAmortizationValue)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.PrepaymentStartStatusStatementNumber).IsOptionalPositive(ContractCmts.PrepaymentStartStatusStatementNumber);
        RuleFor(x => x.PrepaymentStartProgressPercentage).IsOptionalPositive(ContractCmts.PrepaymentStartProgressPercentage)
            .PrecisionScale(ContractFinancialMath.PercentagePrecision, ContractFinancialMath.PercentageScale, true)
            .Must(x => !x.HasValue || x.Value <= 100m).WithError(ContractErrors.ContractFinancialTermsInvalid);
        RuleFor(x => x).Must(IsConsistent).WithError(ContractErrors.ContractFinancialTermsInvalid);
    }

    private static bool IsConsistent(ContractRegistrationFinancialRequest x)
    {
        if (x.AdjustmentLimitValue.HasValue != x.AdjustmentLimitType.HasValue)
            return false;
        if (x.AdjustmentLimitType == ContractAdjustmentLimitType.Percentage &&
            x.AdjustmentLimitValue!.Value > 100m)
            return false;
        if (!x.HasPrepayment)
            return !x.PrepaymentPercentage.HasValue && !x.PrepaymentAmortizationMethod.HasValue &&
                   !x.PrepaymentAmortizationValue.HasValue && !x.PrepaymentStartStatusStatementNumber.HasValue &&
                   !x.PrepaymentStartProgressPercentage.HasValue;
        if (!x.PrepaymentPercentage.HasValue || !x.PrepaymentAmortizationMethod.HasValue || !x.PrepaymentAmortizationValue.HasValue)
            return false;
        return x.PrepaymentAmortizationMethod switch
        {
            Engineering.Domain.Entities.Contracts.Enums.PrepaymentAmortizationMethod.FixedPercentagePerStatusStatement =>
                x.PrepaymentStartStatusStatementNumber.HasValue && !x.PrepaymentStartProgressPercentage.HasValue && x.PrepaymentAmortizationValue.Value <= 100m,
            Engineering.Domain.Entities.Contracts.Enums.PrepaymentAmortizationMethod.FixedAmountPerStatusStatement =>
                x.PrepaymentStartStatusStatementNumber.HasValue && !x.PrepaymentStartProgressPercentage.HasValue,
            Engineering.Domain.Entities.Contracts.Enums.PrepaymentAmortizationMethod.AfterSpecificProgressPercentage =>
                x.PrepaymentStartProgressPercentage.HasValue && !x.PrepaymentStartStatusStatementNumber.HasValue,
            _ => false
        };
    }
}
