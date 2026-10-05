namespace Engineering.Application.Services.Projects.Models.GetProjectProgress;

public class GetProjectProgressValidator : AbstractValidator<GetProjectProgressRequest>
{
    public GetProjectProgressValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}