namespace Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;

public class CreateProjectTypeValidator : AbstractValidator<CreateProjectTypeRequest>
{
    public CreateProjectTypeValidator()
    {
        RuleFor(oo => oo.ProjectTypeCode)
            .IsFullString(ProjectCmts.ProjectTypeCode, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.ProjectTypeName)
            .IsFullString(ProjectCmts.ProjectTypeTitle, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
