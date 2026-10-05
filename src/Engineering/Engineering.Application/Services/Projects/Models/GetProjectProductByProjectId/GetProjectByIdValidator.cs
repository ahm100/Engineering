namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public class GetProjectProductByProjectIdValidator : AbstractValidator<GetProjectProductByProjectIdRequest>
{
    public GetProjectProductByProjectIdValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(ProjectCmts.ProjectId);
    }
}
