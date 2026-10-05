namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProduct;

public class UpdateFiduciaryProductCommandValidator : AbstractValidator<UpdateFiduciaryProductCommand>
{
    public UpdateFiduciaryProductCommandValidator()
    {
        RuleFor(oo => oo.Project).NotEmpty().WithError(FiduciaryProductErrors.InValidProject);
        RuleFor(oo => oo.ProjectOperation).NotEmpty().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleFor(oo => oo.FiduciaryProduct).NotEmpty().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
