namespace Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;

public class GetProjectCategoryProductByProjectIdValidator : AbstractValidator<GetProjectCategoryProductByProjectIdRequest>
{
    public GetProjectCategoryProductByProjectIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(ProjectCmts.ProjectId);
    }
}
