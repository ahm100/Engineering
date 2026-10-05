namespace Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;

public class SetPlannedDateValidator : AbstractValidator<SetPlannedDateRequest>
{
    public SetPlannedDateValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}