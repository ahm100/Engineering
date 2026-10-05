namespace Engineering.Application.Services.OperationInfoGroups.Commands.StateChangerOperationInfoGroups;

public class StateChangerOperationInfoGroupsCommandValidator : AbstractValidator<StateChangerOperationInfoGroupsCommand>
{
    public StateChangerOperationInfoGroupsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
