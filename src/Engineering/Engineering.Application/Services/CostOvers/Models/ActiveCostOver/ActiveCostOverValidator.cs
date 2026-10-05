namespace Engineering.Application.Services.CostOvers.Models.ActiveCostOver;

public class ActiveCostOverValidator : AbstractValidator<ActiveCostOverRequest>
{
    public ActiveCostOverValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}