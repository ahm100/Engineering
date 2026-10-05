
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSupplies;

public class GetFilteredRequestGoodsSuppliesQueryValidator : AbstractValidator<GetFilteredRequestGoodsSuppliesQuery>
{
    public GetFilteredRequestGoodsSuppliesQueryValidator()
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
