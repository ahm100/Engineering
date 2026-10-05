namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturnDocument;

public class CreateFiduciaryProductDetailReturnDocumentCommandValidator : AbstractValidator<CreateFiduciaryProductDetailReturnDocumentCommand>
{
    public CreateFiduciaryProductDetailReturnDocumentCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductReturnDetail).NotNull().WithError(FiduciaryProductDetailReturnDocumentErrors.InValidFiduciaryProductReturnDetail);
        RuleFor(oo => oo.Url).NotNull().WithError(FiduciaryProductDetailReturnDocumentErrors.InValidUrl);
    }
}
