namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduleColumn;

public class RemoveProjectScheduleColumnCommandValidator : AbstractValidator<RemoveProjectScheduleColumnCommand>
{
    public RemoveProjectScheduleColumnCommandValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
