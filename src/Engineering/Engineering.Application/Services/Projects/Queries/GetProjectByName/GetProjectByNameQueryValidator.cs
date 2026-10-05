namespace Engineering.Application.Services.Projects.Queries.GetProjectByName;

public class GetProjectByNameQueryValidator : AbstractValidator<GetProjectByNameQuery>
{
    public GetProjectByNameQueryValidator()
    {
        RuleFor(oo => oo.ProjectName).NotEmpty().WithError(ProjectErrors.ProjectNameIsEmpty);
    }
}