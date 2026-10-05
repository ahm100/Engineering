namespace Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;

public class ActiveCostCenterValidator : AbstractValidator<ActiveCostCenterRequest>
{
    public ActiveCostCenterValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
