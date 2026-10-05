using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPercentComplete;

public class EditPercentCompleteCommandValidator : AbstractValidator<EditPercentCompleteCommand>
{
    public EditPercentCompleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
