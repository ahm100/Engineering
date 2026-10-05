namespace Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;

public class GetAllowedSubProjectManagersValidator : AbstractValidator<GetAllowedSubProjectManagersRequest>
{
    public GetAllowedSubProjectManagersValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}
