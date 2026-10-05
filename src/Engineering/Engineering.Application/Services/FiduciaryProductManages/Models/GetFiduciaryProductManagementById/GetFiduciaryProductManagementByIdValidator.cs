namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public class GetFiduciaryProductManagementByIdValidator : AbstractValidator<GetFiduciaryProductManagementByIdRequest>
{
    public GetFiduciaryProductManagementByIdValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
