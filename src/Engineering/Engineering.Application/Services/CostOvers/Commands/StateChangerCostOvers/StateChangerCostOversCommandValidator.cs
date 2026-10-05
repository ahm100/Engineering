namespace Engineering.Application.Services.CostOvers.Commands.StateChangerCostOvers;

public class StateChangerCostOversCommandValidator : AbstractValidator<StateChangerCostOversCommand>
{
    public StateChangerCostOversCommandValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}