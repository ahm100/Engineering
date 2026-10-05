namespace Engineering.Application.Services.CostCenters.Models.GetCostCenters;

public class GetCostCentersValidator : AbstractValidator<GetCostCentersRequest>
{
    public GetCostCentersValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
