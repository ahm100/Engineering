namespace Engineering.Application.Services.Projects.Queries.GetSummarizedProjectById;

public class GetSummarizedProjectByIdQueryValidator : AbstractValidator<GetSummarizedProjectByIdQuery>
{
    public GetSummarizedProjectByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
