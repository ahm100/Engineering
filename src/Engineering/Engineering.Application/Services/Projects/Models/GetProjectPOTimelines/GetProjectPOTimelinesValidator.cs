namespace Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;

public class GetProjectPOTimelinesValidator : AbstractValidator<GetProjectPOTimelinesRequest>
{
    public GetProjectPOTimelinesValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}
