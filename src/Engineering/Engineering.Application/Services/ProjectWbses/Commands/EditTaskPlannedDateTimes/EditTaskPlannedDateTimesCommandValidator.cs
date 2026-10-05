using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskPlannedDateTimes;

public class EditTaskPlannedDateTimesCommandValidator : AbstractValidator<EditTaskPlannedDateTimesCommand>
{
    public EditTaskPlannedDateTimesCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
