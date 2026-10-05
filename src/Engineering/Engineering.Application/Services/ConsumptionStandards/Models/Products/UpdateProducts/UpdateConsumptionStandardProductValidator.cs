namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.UpdateProduct;

public class UpdateConsumptionStandardProductValidator : AbstractValidator<UpdateConsumptionStandardProductRequest>
{
    public UpdateConsumptionStandardProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
        RuleFor(oo => oo.OperationInfoGoodsId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
        RuleFor(oo => oo.GoodsNumber).GreaterThan(0).WithError(ProductStandardErrors.ProductNumberIsEmpty);
        RuleFor(oo => oo.StandardProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(ProductStandardErrors.TypeIsEmpty);
    }
}
