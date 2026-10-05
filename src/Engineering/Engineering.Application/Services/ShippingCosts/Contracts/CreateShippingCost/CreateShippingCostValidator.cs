namespace Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;

public class CreateShippingCostValidator : AbstractValidator<CreateShippingCostRequest>
{
    public CreateShippingCostValidator()
    {
        RuleFor(c => c.TransportationContractorId)
            .IsPositive(ShippingCostCmts.ShippingCostId);

        RuleForEach(c => c.Shippingcosts)
            .SetValidator(new CreateShippingCostModelValidator());
    }
}
public class CreateShippingCostModelValidator : AbstractValidator<CreateShippingCostModel>
{
    public CreateShippingCostModelValidator()
    {
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

        RuleFor(c => c.Price)
            .IsRequiredDecimal(ShippingCostCmts.Price);
    }
}