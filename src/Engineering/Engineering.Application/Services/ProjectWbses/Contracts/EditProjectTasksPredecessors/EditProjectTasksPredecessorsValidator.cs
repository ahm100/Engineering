namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectTasksPredecessors;

public class EditProjectTasksPredecessorsValidator : AbstractValidator<EditProjectTasksPredecessorsRequest>
{
    public EditProjectTasksPredecessorsValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
