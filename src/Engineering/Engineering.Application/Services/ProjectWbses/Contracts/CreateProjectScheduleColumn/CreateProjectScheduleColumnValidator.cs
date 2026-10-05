namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;

public class CreateProjectScheduleColumnValidator : AbstractValidator<CreateProjectScheduleColumnRequest>
{
    public CreateProjectScheduleColumnValidator()
    {
        RuleFor(e => e.ProjectId)
            .IsPositive(GlobalCmts.Id);

        RuleFor(e => e.TargetColumnId)
            .IsOptionalPositive(GlobalCmts.Id);

        RuleFor(e => e.TitleFa)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(e => e.DataType)
            .IsEnum(WbsCmts.DataType);
    }
}
