
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductHistoryById;

public class GetGoodsSupplyProductHistoryByIdQueryValidator : AbstractValidator<GetGoodsSupplyProductHistoryByIdQuery>
{
    public GetGoodsSupplyProductHistoryByIdQueryValidator()
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
