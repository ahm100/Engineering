namespace Engineering.Application.Services.CostOvers.Commands.InactiveCostOver;

public class InactiveCostOverCommandValidator : AbstractValidator<InactiveCostOverCommand>
{
    public InactiveCostOverCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}