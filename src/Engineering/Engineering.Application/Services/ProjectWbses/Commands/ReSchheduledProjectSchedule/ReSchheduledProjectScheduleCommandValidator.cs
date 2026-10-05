using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.ReSchheduledProjectSchedule;

public class ReSchheduledProjectScheduleCommandValidator : AbstractValidator<ReSchheduledProjectScheduleCommand>
{
    public ReSchheduledProjectScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
