
namespace Engineering.Application.Services.CostCenters.Models.CostCenterGroupDelete;

public class CostCenterGroupDeleteValidator : AbstractValidator<CostCenterGroupDeleteRequest>
{
    public CostCenterGroupDeleteValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
