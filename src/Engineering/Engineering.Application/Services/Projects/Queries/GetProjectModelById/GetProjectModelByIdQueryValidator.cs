namespace Engineering.Application.Services.Projects.Queries.GetProjectModelById;

public class GetProjectModelByIdQueryValidator : AbstractValidator<GetProjectModelByIdQuery>
{
    public GetProjectModelByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
