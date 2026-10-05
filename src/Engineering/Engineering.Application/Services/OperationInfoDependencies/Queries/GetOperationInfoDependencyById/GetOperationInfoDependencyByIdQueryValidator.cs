namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencyById;

public class GetOperationInfoDependencyByIdQueryValidator : AbstractValidator<GetOperationInfoDependencyByIdQuery>
{
    public GetOperationInfoDependencyByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoDependencyErrors.IdIsEmpty);
    }
}