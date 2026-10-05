namespace Engineering.Application.Services.Contracts.Contracts.CreateContractTypeDetail;

using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Errors;

public class CreateContractTypeDetailValidator : AbstractValidator<CreateContractTypeDetailRequest>
{
    public CreateContractTypeDetailValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);

        RuleFor(oo => oo.SourceId)
            .IsPositive(ContractCmts.SourceId);

        RuleFor(oo => oo.Quantity)
            .IsPositive(ContractCmts.Quantity)
            .PrecisionScale(
                ContractFinancialMath.QuantityPrecision,
                ContractFinancialMath.QuantityScale,
                true);

        RuleFor(oo => oo.UnitOfMeasurementId)
            .IsOptionalPositive(ContractCmts.UnitOfMeasurementId);

        RuleFor(oo => oo.UnitPrice)
            .IsOptionalPositive(ContractCmts.UnitPrice)
            .PrecisionScale(
                ContractFinancialMath.MoneyPrecision,
                ContractFinancialMath.MoneyScale,
                true);

        RuleFor(oo => oo.FixedAmount)
            .IsOptionalPositive(ContractCmts.FixedAmount)
            .PrecisionScale(
                ContractFinancialMath.MoneyPrecision,
                ContractFinancialMath.MoneyScale,
                true);

        RuleFor(oo => oo.Duration)
            .IsOptionalPositive(ContractCmts.DetailDuration)
            .PrecisionScale(
                ContractFinancialMath.DurationPrecision,
                ContractFinancialMath.DurationScale,
                true);

        RuleFor(oo => oo.DurationUnit)
            .IsNullableEnum(ContractCmts.DurationUnit);

        RuleFor(oo => oo)
            .Must(oo => oo.Duration.HasValue == oo.DurationUnit.HasValue)
            .WithError(ContractErrors.ContractTypeDetailTermsInvalid);

        RuleFor(oo => oo.Adjustment)
            .SetValidator(new ContractTypeDetailAdjustmentRequestValidator());
    }
}
