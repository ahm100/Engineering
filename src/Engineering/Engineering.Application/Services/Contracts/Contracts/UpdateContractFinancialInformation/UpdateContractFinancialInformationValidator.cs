using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractFinancialInformation;

using Engineering.Domain.Entities.Contracts;

public class UpdateContractFinancialInformationValidator
    : AbstractValidator<UpdateContractFinancialInformationRequest>
{
    public UpdateContractFinancialInformationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.CurrencyId)
            .IsPositive(GlobalCmts.CurrencyId);

        RuleFor(oo => oo.ContractCeilingAmount)
            .IsOptionalPositive(ContractCmts.ContractCeilingAmount)
            .PrecisionScale(
                ContractFinancialMath.MoneyPrecision,
                ContractFinancialMath.MoneyScale,
                true);

        RuleFor(oo => oo.AdjustmentLimitValue)
            .IsOptionalPositive(ContractCmts.AdjustmentLimitValue)
            .PrecisionScale(
                ContractFinancialMath.MoneyPrecision,
                ContractFinancialMath.MoneyScale,
                true);

        RuleFor(oo => oo.AdjustmentLimitType)
            .IsNullableEnum(ContractCmts.AdjustmentLimitType);

        RuleFor(oo => oo.PrepaymentPercentage)
            .IsOptionalPositive(ContractCmts.PrepaymentPercentage)
            .PrecisionScale(
                ContractFinancialMath.PercentagePrecision,
                ContractFinancialMath.PercentageScale,
                true)
            .Must(value => !value.HasValue || value.Value <= 100m)
            .WithError(ContractErrors.ContractFinancialTermsInvalid);

        RuleFor(oo => oo.PrepaymentAmortizationMethod)
            .IsNullableEnum(ContractCmts.PrepaymentAmortizationMethod);

        RuleFor(oo => oo.PrepaymentAmortizationValue)
            .IsOptionalPositive(ContractCmts.PrepaymentAmortizationValue)
            .PrecisionScale(
                ContractFinancialMath.MoneyPrecision,
                ContractFinancialMath.MoneyScale,
                true);

        RuleFor(oo => oo.PrepaymentStartStatusStatementNumber)
            .IsOptionalPositive(ContractCmts.PrepaymentStartStatusStatementNumber);

        RuleFor(oo => oo.PrepaymentStartProgressPercentage)
            .IsOptionalPositive(ContractCmts.PrepaymentStartProgressPercentage)
            .PrecisionScale(
                ContractFinancialMath.PercentagePrecision,
                ContractFinancialMath.PercentageScale,
                true)
            .Must(value => !value.HasValue || value.Value <= 100m)
            .WithError(ContractErrors.ContractFinancialTermsInvalid);

        RuleFor(oo => oo)
            .Must(HasValidFinancialTerms)
            .WithError(ContractErrors.ContractFinancialTermsInvalid);
    }

    private static bool HasValidFinancialTerms(
        UpdateContractFinancialInformationRequest request)
    {
        if (request.AdjustmentLimitValue.HasValue != request.AdjustmentLimitType.HasValue)
            return false;

        if (request.AdjustmentLimitType == ContractAdjustmentLimitType.Percentage &&
            request.AdjustmentLimitValue!.Value > 100m)
        {
            return false;
        }

        if (!request.HasPrepayment)
        {
            return !request.PrepaymentPercentage.HasValue &&
                   !request.PrepaymentAmortizationMethod.HasValue &&
                   !request.PrepaymentAmortizationValue.HasValue &&
                   !request.PrepaymentStartStatusStatementNumber.HasValue &&
                   !request.PrepaymentStartProgressPercentage.HasValue;
        }

        if (!request.PrepaymentPercentage.HasValue ||
            !request.PrepaymentAmortizationMethod.HasValue ||
            !request.PrepaymentAmortizationValue.HasValue)
        {
            return false;
        }

        return request.PrepaymentAmortizationMethod.Value switch
        {
            PrepaymentAmortizationMethod.FixedPercentagePerStatusStatement =>
                request.PrepaymentStartStatusStatementNumber.HasValue &&
                !request.PrepaymentStartProgressPercentage.HasValue &&
                request.PrepaymentAmortizationValue.Value <= 100m,

            PrepaymentAmortizationMethod.FixedAmountPerStatusStatement =>
                request.PrepaymentStartStatusStatementNumber.HasValue &&
                !request.PrepaymentStartProgressPercentage.HasValue,

            PrepaymentAmortizationMethod.AfterSpecificProgressPercentage =>
                request.PrepaymentStartProgressPercentage.HasValue &&
                !request.PrepaymentStartStatusStatementNumber.HasValue,

            _ => false
        };
    }
}
