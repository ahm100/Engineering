namespace Engineering.Application.Services.FiduciaryProductManages.Commands.DeleteFiduciaryProductDetailReturnDocument;

public class DeleteFiduciaryProductDetailReturnDocumentCommandValidator : AbstractValidator<DeleteFiduciaryProductDetailReturnDocumentCommand>
{
    public DeleteFiduciaryProductDetailReturnDocumentCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnDocumentId).NotNull().WithError(FiduciaryProductDetailReturnDocumentErrors.InValidFiduciaryProductDetaiReturnDocument);
    }
}
