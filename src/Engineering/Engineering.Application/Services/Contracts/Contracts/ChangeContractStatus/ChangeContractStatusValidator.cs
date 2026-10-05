using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractStatus;

public class ChangeContractStatusValidator : AbstractValidator<ChangeContractStatusRequest>
{
    public ChangeContractStatusValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.Operation)
            .IsEnum(ContractCmts.TransitionType);

        When(oo => oo.Operation == ContractStatusTransitionType.Suspend, () =>
        {
            RuleFor(oo => oo.EffectiveDate)
                .NotNull()
                .WithMessage(ContractCmts.EffectiveDate);
            RuleFor(oo => oo.SuspensionDurationMonths)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(ContractCmts.SuspensionDurationMonths);
            RuleFor(oo => oo.Reason)
                .NotEmpty()
                .WithMessage(ContractCmts.Reason);
        });

        When(oo => oo.Operation is ContractStatusTransitionType.Finish
            or ContractStatusTransitionType.Terminate, () =>
        {
            RuleFor(oo => oo.EffectiveDate)
                .NotNull()
                .WithMessage(ContractCmts.EffectiveDate);
            RuleFor(oo => oo.Reason)
                .NotEmpty()
                .WithMessage(ContractCmts.Reason);
        });
    }
}
