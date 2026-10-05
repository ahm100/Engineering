namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduleColumn;

public class CreateProjectScheduleColumnCommandValidator : AbstractValidator<CreateProjectScheduleColumnCommand>
{
    public CreateProjectScheduleColumnCommandValidator()
    {
        RuleFor(e => e.ProjectId)
            .IsPositive(GlobalCmts.Id);

        RuleFor(e => e.TargetColumnId)
            .IsOptionalPositive(GlobalCmts.Id);

        RuleFor(e => e.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(e => e.DataType)
            .IsEnum(WbsCmts.DataType);
    }
}
