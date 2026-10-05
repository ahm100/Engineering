namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceByIdNoInclude;

public class GetProjectServiceByIdNoIncludeQueryValidator : AbstractValidator<GetProjectServiceByIdNoIncludeQuery>
{
    public GetProjectServiceByIdNoIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectServiceErrors.IdIsEmpty);
    }
}