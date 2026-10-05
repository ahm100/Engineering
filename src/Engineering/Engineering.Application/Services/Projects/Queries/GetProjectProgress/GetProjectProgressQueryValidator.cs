namespace Engineering.Application.Services.Projects.Queries.GetProjectProgress;

public class GetProjectProgressQueryValidator : AbstractValidator<GetProjectProgressQuery>
{
    public GetProjectProgressQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}