namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterById;

public class GetCostCenterByIdValidator : AbstractValidator<GetCostCenterByIdRequest>
{
    public GetCostCenterByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
