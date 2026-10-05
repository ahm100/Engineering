namespace Engineering.Application.Services.ProjectTypes.Queries.GetByCode;

public class GetProjectTypeByCodeQueryValidator : AbstractValidator<GetProjectTypeByCodeQuery>
{
    public GetProjectTypeByCodeQueryValidator()
    {
        RuleFor(oo => oo.ProjectTypeCode).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeCodeIsEmpty);
    }
}
