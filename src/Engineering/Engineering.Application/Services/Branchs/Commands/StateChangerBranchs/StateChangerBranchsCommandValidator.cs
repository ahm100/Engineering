namespace Engineering.Application.Services.Branchs.Commands.StateChangerBranchs;

public class StateChangerBranchsCommandValidator : AbstractValidator<StateChangerBranchsCommand>
{
    public StateChangerBranchsCommandValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}