namespace Engineering.Application.Services.Projects.Queries.GetLastProjectCode;

public class GetLastProjectCodeQueryValidator : AbstractValidator<GetLastProjectCodeQuery>
{
    public GetLastProjectCodeQueryValidator()
    {
        RuleFor(oo => oo.EmployerCode)
            .IsFullString(ProjectCmts.EmployerCode, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}