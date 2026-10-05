namespace Engineering.Application.Services.CostOvers.Models.StateChangerCostOvers;

public class StateChangerCostOversValidator : AbstractValidator<StateChangerCostOversRequest>
{
    public StateChangerCostOversValidator()
    {
        RuleFor(v => v.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(v => v.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}