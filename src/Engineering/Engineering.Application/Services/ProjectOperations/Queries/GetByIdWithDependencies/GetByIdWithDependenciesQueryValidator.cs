namespace Engineering.Application.Services.ProjectOperations.Queries.GetByIdWithDependencies;

public class GetByIdWithDependenciesQueryValidator : AbstractValidator<GetByIdWithDependenciesQuery>
{
    public GetByIdWithDependenciesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
