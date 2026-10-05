namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceById;

public class GetProjectServiceByIdQueryValidator : AbstractValidator<GetProjectServiceByIdQuery>
{
    public GetProjectServiceByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectServiceErrors.IdIsEmpty);
    }
}