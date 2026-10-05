
namespace Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;

public class ProjectDocCodeGeneratorValidator : AbstractValidator<ProjectDocCodeGeneratorRequest>
{
    public ProjectDocCodeGeneratorValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(c => c.DisciplineId)
            .IsPositive(GlobalCmts.DisciplineId);

        RuleFor(c => c.DisciplineDocId)
            .IsPositive(GlobalCmts.DisciplineDocId);
    }
}