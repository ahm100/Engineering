namespace Engineering.Application.Services.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}