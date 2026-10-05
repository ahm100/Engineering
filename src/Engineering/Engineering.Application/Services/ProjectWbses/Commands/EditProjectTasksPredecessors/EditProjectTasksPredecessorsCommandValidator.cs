using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectTasksPredecessors;

public class EditProjectTasksPredecessorsCommandValidator : AbstractValidator<EditProjectTasksPredecessorsCommand>
{
    public EditProjectTasksPredecessorsCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
