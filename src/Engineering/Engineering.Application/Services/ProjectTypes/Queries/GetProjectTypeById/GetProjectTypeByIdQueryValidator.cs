namespace Engineering.Application.Services.ProjectTypes.Queries.GetProjectTypeById;

public class GetProjectTypeByIdQueryValidator : AbstractValidator<GetProjectTypeByIdQuery>
{
    public GetProjectTypeByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectTypeErrors.IdIsEmpty);
    }
}