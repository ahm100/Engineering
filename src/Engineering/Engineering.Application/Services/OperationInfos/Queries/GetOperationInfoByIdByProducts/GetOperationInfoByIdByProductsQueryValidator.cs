namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProducts;

public class GetOperationInfoByIdByProductsQueryValidator : AbstractValidator<GetOperationInfoByIdByProductsQuery>
{
    public GetOperationInfoByIdByProductsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
