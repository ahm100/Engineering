namespace Engineering.Application.Services.CostOvers.Commands.DisableCostOver;

public class DisableCostOverCommandValidator : AbstractValidator<DisableCostOverCommand>
{
    public DisableCostOverCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}