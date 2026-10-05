namespace Engineering.Application.Services.OperationInfos.Commands.StateChangerOperationInfos;

public class StateChangerOperationInfosCommandValidator : AbstractValidator<StateChangerOperationInfosCommand>
{
    public StateChangerOperationInfosCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
