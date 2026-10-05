namespace Engineering.Application.Services.CostOvers.Commands.ActiveCostOver;

public class ActiveCostOverCommandValidator : AbstractValidator<ActiveCostOverCommand>
{
    public ActiveCostOverCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}