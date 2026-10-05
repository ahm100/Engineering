namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdWithChild;

public class GetOperationInfoByIdWithChildQueryValidator : AbstractValidator<GetOperationInfoByIdWithChildQuery>
{
    public GetOperationInfoByIdWithChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
