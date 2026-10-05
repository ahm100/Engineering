namespace Engineering.Application.Services.ProjectServices.Commands.StateChangerProjectServices;

public class StateChangerProjectServicesCommandValidator : AbstractValidator<StateChangerProjectServicesCommand>
{
    public StateChangerProjectServicesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
