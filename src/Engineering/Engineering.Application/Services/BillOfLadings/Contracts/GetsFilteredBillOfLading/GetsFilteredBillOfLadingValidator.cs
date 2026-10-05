namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;

public class GetsFilteredBillOfLadingValidator : AbstractValidator<GetsFilteredBillOfLadingRequest>
{
    public GetsFilteredBillOfLadingValidator()
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