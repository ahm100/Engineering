namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;

public class GetCostCenterHistoriesValidator : AbstractValidator<GetCostCenterHistoriesRequest>
{
    public GetCostCenterHistoriesValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
