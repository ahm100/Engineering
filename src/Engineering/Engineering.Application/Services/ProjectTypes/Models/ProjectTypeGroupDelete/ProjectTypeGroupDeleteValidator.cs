
namespace Engineering.Application.Services.ProjectTypes.Models.ProjectTypeGroupDelete;

public class ProjectTypeGroupDeleteValidator : AbstractValidator<ProjectTypeGroupDeleteRequest>
{
    public ProjectTypeGroupDeleteValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(ProjectCmts.ProjectTypeId);
    }
}
