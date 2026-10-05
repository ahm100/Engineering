namespace Engineering.Application.Services.ProjectOperations.Command.SetPlannedDate;

public class SetPlannedDateCommandValidator : AbstractValidator<SetPlannedDateCommand>
{
    public SetPlannedDateCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}