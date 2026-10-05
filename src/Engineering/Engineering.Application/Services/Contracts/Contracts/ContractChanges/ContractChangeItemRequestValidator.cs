namespace Engineering.Application.Services.Contracts.Contracts.ContractChanges;

using Engineering.Domain.Entities.Contracts;

public class ContractChangeItemRequestValidator
    : AbstractValidator<ContractChangeItemRequest>
{
    public ContractChangeItemRequestValidator()
    {
        RuleFor(oo => oo)
            .Must(oo => oo.ContractTypeDetailId.HasValue != (oo.SourceItem is not null))
            .WithError(ContractErrors.ContractChangeItemInvalid);

        RuleFor(oo => oo.ContractTypeDetailId)
            .IsOptionalPositive(ContractCmts.ContractTypeDetail);

        RuleFor(oo => oo.SourceItem)
            .SetValidator(new ContractChangeSourceItemRequestValidator());

        RuleFor(oo => oo.NewValue)
            .GreaterThanOrEqualTo(0m)
            .WithError(ContractErrors.ContractChangeItemInvalid)
            .PrecisionScale(
                ContractFinancialMath.ChangeValuePrecision,
                ContractFinancialMath.ChangeValueScale,
                true);
    }
}
