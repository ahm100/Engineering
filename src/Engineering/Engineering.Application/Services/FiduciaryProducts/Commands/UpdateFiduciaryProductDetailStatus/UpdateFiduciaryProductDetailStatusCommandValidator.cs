namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductDetailStatus;

public class UpdateFiduciaryProductDetailStatusCommandValidator : AbstractValidator<UpdateFiduciaryProductDetailStatusCommand>
{
    public UpdateFiduciaryProductDetailStatusCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
        RuleFor(oo => oo.Status).IsInEnum().WithError(FiduciaryProductDetailErrors.InValidStatus);
    }
}
