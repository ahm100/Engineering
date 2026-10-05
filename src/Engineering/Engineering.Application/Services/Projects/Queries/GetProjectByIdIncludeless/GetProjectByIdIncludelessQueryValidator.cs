namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;

public class GetProjectByIdIncludelessQueryValidator : AbstractValidator<GetProjectByIdIncludelessQuery>
{
    public GetProjectByIdIncludelessQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}