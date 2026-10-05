namespace Engineering.Application.Services.FiduciaryProducts.Commands.CreateFiduciaryProduct;

public class CreateFiduciaryProductCommandValidator : AbstractValidator<CreateFiduciaryProductCommand>
{
    public CreateFiduciaryProductCommandValidator()
    {
        RuleFor(oo => oo.Project).NotEmpty().WithError(FiduciaryProductErrors.InValidProject);
        RuleFor(oo => oo.ProjectOperation).NotEmpty().WithError(FiduciaryProductErrors.InValidProjectOperation);
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(FiduciaryProductErrors.InValidThirdPartyId);
    }
}
