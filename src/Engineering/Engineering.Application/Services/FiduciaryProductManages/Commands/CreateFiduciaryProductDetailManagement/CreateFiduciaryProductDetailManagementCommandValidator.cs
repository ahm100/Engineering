namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailManagement;

public class CreateFiduciaryProductDetailManagementCommandValidator : AbstractValidator<CreateFiduciaryProductDetailManagementCommand>
{
    public CreateFiduciaryProductDetailManagementCommandValidator()
    {
        RuleFor(oo => oo.ConfirmedLoanCount).GreaterThan(0).WithError(FiduciaryProductDetailManagementErrors.InValidConfirmedLoanCount);
        RuleFor(oo => oo.InvoiceId).NotNull().WithError(FiduciaryProductDetailManagementErrors.InValidInvoiceId);
        RuleFor(oo => oo.WarehouseId).NotNull().WithError(FiduciaryProductDetailManagementErrors.InValidWarehouseId);
        RuleFor(oo => oo.FiduciaryProductDetail).NotEmpty().WithError(FiduciaryProductDetailManagementErrors.InValidFiduciaryProductDetail);
    }
}
