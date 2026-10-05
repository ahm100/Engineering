
namespace Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;

public class StateChangerCostCentersValidator : AbstractValidator<StateChangerCostCentersRequest>
{
    public StateChangerCostCentersValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
