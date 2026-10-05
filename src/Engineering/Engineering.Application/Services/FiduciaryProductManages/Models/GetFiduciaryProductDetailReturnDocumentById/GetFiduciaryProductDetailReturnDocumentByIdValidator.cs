namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnDocumentById;

public class GetFiduciaryProductDetailReturnDocumentByIdValidator : AbstractValidator<GetFiduciaryProductDetailReturnDocumentByIdRequest>
{
    public GetFiduciaryProductDetailReturnDocumentByIdValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnDocumentErrors.InValidFiduciaryProductReturnDetail);
    }
}
