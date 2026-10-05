namespace Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;

public class DeleteShippingCostValidator : AbstractValidator<DeleteShippingCostRequest>
{
    public DeleteShippingCostValidator()
    {
        RuleForEach(oo => oo.Ids)
            .NotNull().WithError(ShippingCostErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ShippingCostErrors.IdIsEmpty);
    }
}
