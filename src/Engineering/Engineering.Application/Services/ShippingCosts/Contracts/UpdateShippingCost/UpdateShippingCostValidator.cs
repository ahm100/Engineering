namespace Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;

public class UpdateShippingCostValidator : AbstractValidator<UpdateShippingCostRequest>
{
    public UpdateShippingCostValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(ShippingCostCmts.ShippingCostId);

        RuleFor(c => c.TransportationContractorId)
            .IsPositive(ShippingCostCmts.ShippingCostId);

        RuleFor(c => c.MachineTypeId)
            .IsPositive(ShippingCostCmts.MachineTypeId);

        When(x => x.SourceCityId != null && x.SourceCityId > 0, () =>
        {
            RuleFor(c => c.SourceCityId!.Value)
                .IsPositive(ShippingCostCmts.SourceCity);
        });

        When(x => x.DestinationCityId != null && x.DestinationCityId > 0, () =>
        {
            RuleFor(c => c.DestinationCityId!.Value)
                .IsPositive(ShippingCostCmts.DestinationCity);
        });

        RuleFor(c => c.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);

        RuleFor(c => c.Price)
            .IsRequiredDecimal(ShippingCostCmts.Price);
    }
}
