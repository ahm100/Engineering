namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduleColumn;

public class RemoveProjectScheduleColumnValidator : AbstractValidator<RemoveProjectScheduleColumnRequest>
{
    public RemoveProjectScheduleColumnValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
