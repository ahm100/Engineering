namespace Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;

public class GetFilteredCostCenterCitiesValidator : AbstractValidator<GetFilteredCostCenterCitiesRequest>
{
    public GetFilteredCostCenterCitiesValidator()
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
