namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleStartDate;

public class EditProjectScheduleStartDateCommandValidator : AbstractValidator<EditProjectScheduleStartDateCommand>
{
    public EditProjectScheduleStartDateCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
