namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;

public class GetsActiveBillOfLadingValidator : AbstractValidator<GetsActiveBillOfLadingRequest>
{
    public GetsActiveBillOfLadingValidator()
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