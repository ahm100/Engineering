namespace Engineering.Application.Services.CabinTypes.Commands.StateChangerCabinTypes;

public class StateChangerCabinTypesCommandValidator : AbstractValidator<StateChangerCabinTypesCommand>
{
    public StateChangerCabinTypesCommandValidator()
    {
        RuleFor(v => v.Items)
            .NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}