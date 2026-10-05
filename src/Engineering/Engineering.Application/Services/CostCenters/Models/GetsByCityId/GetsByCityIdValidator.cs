namespace Engineering.Application.Services.CostCenters.Models.GetsByCityId;

public class GetsByCityIdValidator : AbstractValidator<GetsByCityIdRequest>
{
    public GetsByCityIdValidator()
    {
        RuleFor(oo => oo.CityId)
            .IsPositive(CCenterCmts.EmployerId);
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
