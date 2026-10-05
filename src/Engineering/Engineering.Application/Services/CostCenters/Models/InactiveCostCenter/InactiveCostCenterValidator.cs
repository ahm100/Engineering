namespace Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;

public class InactiveCostCenterValidator : AbstractValidator<InactiveCostCenterRequest>
{
    public InactiveCostCenterValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
