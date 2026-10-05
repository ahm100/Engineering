namespace Engineering.Application.Services.CostCenters.Models.GetsByTypeId;

public class GetsByTypeIdValidator : AbstractValidator<GetsByTypeIdRequest>
{
    public GetsByTypeIdValidator()
    {
        RuleFor(oo => oo.CostCenterTypeId)
            .IsPositive(CCenterCmts.CostCenterTypeId);
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
