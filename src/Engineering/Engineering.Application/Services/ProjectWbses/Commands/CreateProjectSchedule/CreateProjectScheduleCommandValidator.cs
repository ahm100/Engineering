using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectSchedule;

public class CreateProjectScheduleCommandValidator : AbstractValidator<CreateProjectScheduleCommand>
{
    public CreateProjectScheduleCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);

        RuleFor(x => x.UserId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
