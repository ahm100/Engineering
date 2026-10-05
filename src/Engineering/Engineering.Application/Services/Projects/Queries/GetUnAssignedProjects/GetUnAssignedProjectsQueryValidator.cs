namespace Engineering.Application.Services.Projects.Queries.GetUnAssignedProjects;

public class GetUnAssignedProjectsQueryValidator : AbstractValidator<GetUnAssignedProjectsQuery>
{
    public GetUnAssignedProjectsQueryValidator()
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