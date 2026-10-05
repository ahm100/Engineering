namespace Engineering.Application.Services.Projects.Queries.GetProjectPOTimelines;

public class GetProjectPOTimelinesQueryValidator : AbstractValidator<GetProjectPOTimelinesQuery>
{
    public GetProjectPOTimelinesQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}
