namespace Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

public class UpdateFiduciaryProductValidator : AbstractValidator<UpdateFiduciaryProductRequest>
{
    public UpdateFiduciaryProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
        RuleFor(oo => oo.CostCenterId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(FiduciaryProductErrors.InValidCostCenter);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductErrors.InValidProject);
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleForEach(oo => oo.Products).NotEmpty().SetValidator(new UpdateFiduciaryProductDetailModelValidator());
    }
}
