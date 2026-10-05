namespace Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;

public class GetActiveCostCentersValidator : AbstractValidator<GetActiveCostCentersRequest>
{
    public GetActiveCostCentersValidator()
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
