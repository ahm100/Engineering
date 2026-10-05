namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;

public class GetProjectTypeByNameValidator : AbstractValidator<GetProjectTypeByNameRequest>
{
    public GetProjectTypeByNameValidator()
    {
        RuleFor(oo => oo.ProjectTypeName)
            .IsFullString(ProjectCmts.ProjectTypeTitle, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}
