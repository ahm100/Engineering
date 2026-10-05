namespace Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;

public class GetShippingCostByIdValidator : AbstractValidator<GetShippingCostByIdRequest>
{
    public GetShippingCostByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(ShippingCostErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ShippingCostErrors.IdIsEmpty);
    }
}
