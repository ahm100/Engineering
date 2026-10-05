namespace Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;

public class GetsCostCenterByEmployerIdValidator : AbstractValidator<GetsCostCenterByEmployerIdRequest>
{
    public GetsCostCenterByEmployerIdValidator()
    {
        RuleFor(oo => oo.EmployerId)
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
