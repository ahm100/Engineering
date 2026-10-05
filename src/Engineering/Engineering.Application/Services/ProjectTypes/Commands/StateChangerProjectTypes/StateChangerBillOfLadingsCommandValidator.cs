namespace Engineering.Application.Services.ProjectTypes.Commands.StateChangerProjectTypes;

public class StateChangerProjectTypesCommandValidator : AbstractValidator<StateChangerProjectTypesCommand>
{
    public StateChangerProjectTypesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
