namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;

public class GetProjectByIdNoIncludingQueryValidator : AbstractValidator<GetProjectByIdNoIncludingQuery>
{
    public GetProjectByIdNoIncludingQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
