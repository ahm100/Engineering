namespace Engineering.Application.Services.Projects.Models.DeleteProjectProduct;

public class DeleteProjectProductValidator : AbstractValidator<DeleteProjectProductRequest>
{
    public DeleteProjectProductValidator()
    {
        RuleForEach(oo => oo.Ids).IsPositive(ProjectCmts.ProjectId);
    }
}
