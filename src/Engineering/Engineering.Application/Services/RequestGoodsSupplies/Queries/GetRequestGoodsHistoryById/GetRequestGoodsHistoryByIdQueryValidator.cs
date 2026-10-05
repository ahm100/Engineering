
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsHistoryById;

public class GetRequestGoodsHistoryByIdQueryValidator : AbstractValidator<GetRequestGoodsHistoryByIdQuery>
{
    public GetRequestGoodsHistoryByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
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