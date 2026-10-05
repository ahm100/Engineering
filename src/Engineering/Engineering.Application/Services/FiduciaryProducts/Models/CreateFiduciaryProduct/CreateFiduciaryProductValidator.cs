namespace Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;

public class CreateFiduciaryProductValidator : AbstractValidator<CreateFiduciaryProductRequest>
{
    public CreateFiduciaryProductValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(FiduciaryProductErrors.InValidCostCenter);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductErrors.InValidProject);
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleForEach(oo => oo.Products).NotEmpty().SetValidator(new CreateFiduciaryProductDetailModelValidator());
    }
}
