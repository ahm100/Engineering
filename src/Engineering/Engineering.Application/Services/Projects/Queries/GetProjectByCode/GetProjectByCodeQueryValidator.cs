namespace Engineering.Application.Services.Projects.Queries.GetProjectByCode;

public class GetProjectByCodeQueryValidator : AbstractValidator<GetProjectByCodeQuery>
{
    public GetProjectByCodeQueryValidator()
    {
        RuleFor(oo => oo.ProjectCode).NotEmpty().WithError(ProjectErrors.ProjectCodeIsEmpty);
    }
}