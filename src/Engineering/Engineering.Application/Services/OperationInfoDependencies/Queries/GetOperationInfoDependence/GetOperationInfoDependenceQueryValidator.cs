namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependence;

public class GetOperationInfoDependenceQueryValidator : AbstractValidator<GetOperationInfoDependenceQuery>
{
    public GetOperationInfoDependenceQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoDependencyErrors.OperationInfoIdIsEmpty);
    }
}
