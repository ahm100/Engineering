namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.CreateProduct;

public class CreateConsumptionStandardProductValidator : AbstractValidator<CreateConsumptionStandardProductRequest>
{
    public CreateConsumptionStandardProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProductStandardErrors.ProductIdIsEmpty);
        RuleFor(oo => oo.GoodsNumber).GreaterThan(0).WithError(ProductStandardErrors.ProductNumberIsEmpty);
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
        RuleFor(oo => oo.StandardProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(ProductStandardErrors.TypeIsEmpty);
    }
}
