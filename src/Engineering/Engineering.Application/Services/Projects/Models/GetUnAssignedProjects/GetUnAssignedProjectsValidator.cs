namespace Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;

public class GetUnAssignedProjectsValidator : AbstractValidator<GetUnAssignedProjectsRequest>
{
    public GetUnAssignedProjectsValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}