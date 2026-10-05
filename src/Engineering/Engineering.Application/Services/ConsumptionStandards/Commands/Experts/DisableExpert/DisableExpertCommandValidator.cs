namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.DisableExpert;

public class DisableExpertCommandValidator : AbstractValidator<DisableExpertCommand>
{
    public DisableExpertCommandValidator()
    {
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ExpertStandardErrors.ExpertIdIsEmpty);
    }
}