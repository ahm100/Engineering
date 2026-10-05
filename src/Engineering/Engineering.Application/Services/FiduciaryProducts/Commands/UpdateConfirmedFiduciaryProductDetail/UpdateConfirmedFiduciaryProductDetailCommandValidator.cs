namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateConfirmedFiduciaryProductDetail;

public class UpdateConfirmedFiduciaryProductDetailCommandValidator : AbstractValidator<UpdateConfirmedFiduciaryProductDetailCommand>
{
    public UpdateConfirmedFiduciaryProductDetailCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotEmpty().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetailId);
    }
}
