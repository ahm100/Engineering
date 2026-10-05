namespace Engineering.Application.Services.CostCenters.Commands.StateChangerCostCenters;

public class StateChangerCostCentersCommandValidator : AbstractValidator<StateChangerCostCentersCommand>
{
    public StateChangerCostCentersCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
