namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleColumn;

public class EditProjectScheduleColumnValidator : AbstractValidator<EditProjectScheduleColumnRequest>
{
    public EditProjectScheduleColumnValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(e => e.TitleFa)
            .IsFullString(GlobalCmts.TitleFa, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle)
            .When(e => !String.IsNullOrEmpty(e.TitleFa));

        RuleFor(e => e.TitleEn)
            .IsFullString(GlobalCmts.TitleEn, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle)
            .When(e => !String.IsNullOrEmpty(e.TitleEn));

        RuleFor(e => e.Type)
            .IsNullableEnum(WbsCmts.ColumnType);

        RuleFor(e => e.DataType)
            .IsNullableEnum(WbsCmts.DataType);

        RuleFor(e => e.SortOrder)
            .IsOptionalPositive(WbsCmts.SortOrder);
    }
}
