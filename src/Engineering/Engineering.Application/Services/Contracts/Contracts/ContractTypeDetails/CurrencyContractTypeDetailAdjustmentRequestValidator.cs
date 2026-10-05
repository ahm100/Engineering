using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

using Engineering.Domain.Entities.Contracts;

public class CurrencyContractTypeDetailAdjustmentRequestValidator
    : AbstractValidator<CurrencyContractTypeDetailAdjustmentRequest>
{
    public CurrencyContractTypeDetailAdjustmentRequestValidator()
    {
        RuleFor(oo => oo.BaseDate)
            .IsDate(ContractCmts.CurrencyBaseDate);

        RuleFor(oo => oo.BaseRate)
            .IsPositive(ContractCmts.CurrencyBaseRate)
            .PrecisionScale(
                ContractFinancialMath.RatePrecision,
                ContractFinancialMath.RateScale,
                true);

        RuleFor(oo => oo.CurrencyId)
            .IsPositive(GlobalCmts.CurrencyId);

        RuleFor(oo => oo.ReferenceType)
            .IsEnum(ContractCmts.CurrencyReferenceType);

        When(
            oo => oo.ReferenceType is
                ContractAdjustmentCurrencyReferenceType.Contractual or
                ContractAdjustmentCurrencyReferenceType.Other,
            () =>
            {
                RuleFor(oo => oo.CustomReference)
                    .HasMaxLength(ContractCmts.CurrencyCustomReference, 250);
            });

        When(
            oo => oo.ReferenceType is
                ContractAdjustmentCurrencyReferenceType.CentralBank or
                ContractAdjustmentCurrencyReferenceType.ExchangeCenter,
            () =>
            {
                RuleFor(oo => oo.CustomReference)
                    .Must(string.IsNullOrWhiteSpace)
                    .WithError(ContractErrors.ContractTypeDetailAdjustmentTermsInvalid);
            });
    }
}
