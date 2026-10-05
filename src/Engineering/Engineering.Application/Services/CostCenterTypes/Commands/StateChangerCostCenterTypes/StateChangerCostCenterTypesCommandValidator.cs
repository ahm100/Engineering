namespace Engineering.Application.Services.CostCenterTypes.Commands.StateChangerCostCenterTypes;

public class StateChangerCostCenterTypesCommandValidator : AbstractValidator<StateChangerCostCenterTypesCommand>
{
    public StateChangerCostCenterTypesCommandValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}