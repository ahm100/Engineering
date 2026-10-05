namespace Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;

public class UpdateProjectTypeValidator : AbstractValidator<UpdateProjectTypeRequest>
{
    public UpdateProjectTypeValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectTypeId);
        RuleFor(oo => oo.ProjectTypeName)
            .IsFullString(ProjectCmts.ProjectTypeTitle, 100, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(oo => oo.ProjectTypeCode)
            .IsFullString(ProjectCmts.ProjectTypeCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
