namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDataById;

public class GetFiduciaryProductDataByIdQueryValidator : AbstractValidator<GetFiduciaryProductDataByIdQuery>
{
    public GetFiduciaryProductDataByIdQueryValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().NotEmpty().WithError(FiduciaryProductErrors.InValidFiduciaryProductId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
