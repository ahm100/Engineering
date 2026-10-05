namespace Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductStatus;

public class UpdateFiduciaryProductStatusCommandValidator : AbstractValidator<UpdateFiduciaryProductStatusCommand>
{
    public UpdateFiduciaryProductStatusCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
        RuleFor(oo => oo.Status).IsInEnum().WithError(FiduciaryProductErrors.InValidStatus);
    }
}
