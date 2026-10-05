namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrRGS;

public class GetFltrRGSQueryValidator : AbstractValidator<GetFltrRGSQuery>
{
    public GetFltrRGSQueryValidator()
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