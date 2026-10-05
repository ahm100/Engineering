namespace Engineering.Application.Services.ProjectTypes.Queries.GetByName;

public class GetProjectTypeByNameQueryValidator : AbstractValidator<GetProjectTypeByNameQuery>
{
    public GetProjectTypeByNameQueryValidator()
    {
        RuleFor(oo => oo.ProjectTypeName).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeNameIsEmpty);
    }
}
