namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;

public class GetProjectTypeByCodeValidator : AbstractValidator<GetProjectTypeByCodeRequest>
{
    public GetProjectTypeByCodeValidator()
    {
        RuleFor(oo => oo.ProjectTypeCode)
            .IsFullString(ProjectCmts.ProjectTypeTitle, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
