namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleStartDate;

public class EditProjectScheduleStartDateValidator : AbstractValidator<EditProjectScheduleStartDateRequest>
{
    public EditProjectScheduleStartDateValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
