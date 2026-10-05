namespace Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;

public class ChangeShippingCostStateValidator : AbstractValidator<ChangeShippingCostStateRequest>
{
    public ChangeShippingCostStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(ShippingCostCmts.ShippingCostId);
    }
}
