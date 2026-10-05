namespace Engineering.Application.Services.OperationInfos.Queries.GetByIdWithDependencies;

public class GetByIdWithDependenciesQueryValidator : AbstractValidator<GetByIdWithDependenciesQuery>
{
    public GetByIdWithDependenciesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}