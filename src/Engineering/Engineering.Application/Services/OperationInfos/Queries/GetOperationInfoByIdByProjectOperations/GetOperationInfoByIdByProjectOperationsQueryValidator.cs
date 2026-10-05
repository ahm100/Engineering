namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProjectOperations;

public class GetOperationInfoByIdByProjectOperationsQueryValidator : AbstractValidator<GetOperationInfoByIdByProjectOperationsQuery>
{
    public GetOperationInfoByIdByProjectOperationsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
