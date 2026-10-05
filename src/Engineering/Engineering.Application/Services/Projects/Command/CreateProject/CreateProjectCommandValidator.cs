namespace Engineering.Application.Services.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(oo => oo.ProjectName)
            .IsRequiredString(ProjectCmts.ProjectName);
        RuleFor(oo => oo.Status)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
        RuleFor(oo => oo.Contractual)
            .IsRequiredBool(ProjectCmts.Contractual);
    }
}