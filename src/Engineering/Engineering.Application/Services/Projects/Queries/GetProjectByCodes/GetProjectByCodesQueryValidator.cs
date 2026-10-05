namespace Engineering.Application.Services.Projects.Queries.GetProjectByCodes;

public class GetProjectByCodesQueryValidator : AbstractValidator<GetProjectByCodesQuery>
{
    public GetProjectByCodesQueryValidator()
    {
        RuleFor(oo => oo.ProjectCodes).NotEmpty().WithError(ProjectErrors.ProjectCodesIsEmpty);
    }
}