namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;

public class GetOperationInfoByIdIncludeLessQueryValidator : AbstractValidator<GetOperationInfoByIdIncludeLessQuery>
{
    public GetOperationInfoByIdIncludeLessQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
