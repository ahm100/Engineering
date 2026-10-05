namespace Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;

public class CreateFiduciaryProductDetailReturnValidator : AbstractValidator<CreateFiduciaryProductDetailReturnRequest>
{
    public CreateFiduciaryProductDetailReturnValidator()
    {
        RuleForEach(oo => oo.Data).NotEmpty().SetValidator(new CreateFiduciaryProductDetailReturnModelRequestValidator());
    }
}
