namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectSchedule;

public class RemoveProjectScheduleCommandValidator : AbstractValidator<RemoveProjectScheduleCommand>
{
    public RemoveProjectScheduleCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
