using Engineering.Application.Services.ConsumptionStandards.Queries.Products.ProductsByOperationInfoId;

public class ProductsByOperationInfoIdQueryValidator : AbstractValidator<ProductsByOperationInfoIdQuery>
{
    public ProductsByOperationInfoIdQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().WithError(ProductStandardErrors.OprationInfoIdIsEmpty);
    }
}