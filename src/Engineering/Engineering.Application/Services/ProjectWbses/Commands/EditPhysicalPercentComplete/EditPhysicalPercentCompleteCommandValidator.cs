using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPhysicalPercentComplete;

public class EditPhysicalPercentCompleteCommandValidator : AbstractValidator<EditPhysicalPercentCompleteCommand>
{
    public EditPhysicalPercentCompleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
