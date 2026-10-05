namespace Engineering.Application.Services.OperationInfoDependencies.Queries.FindOperationInfoDependencies;

public class FindOperationInfoDependenciesQueryValidator : AbstractValidator<FindOperationInfoDependenciesQuery>
{
    public FindOperationInfoDependenciesQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoDependencyErrors.OperationInfoIdIsEmpty);
    }
}