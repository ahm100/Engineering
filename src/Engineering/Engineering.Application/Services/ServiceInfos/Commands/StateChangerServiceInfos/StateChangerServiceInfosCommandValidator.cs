namespace Engineering.Application.Services.ServiceInfos.Commands.StateChangerServiceInfos;

public class StateChangerServiceInfosCommandValidator : AbstractValidator<StateChangerServiceInfosCommand>
{
    public StateChangerServiceInfosCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
