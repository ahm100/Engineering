
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplySummary;

public class GetRequestGoodsSupplySummaryQueryValidator : AbstractValidator<GetRequestGoodsSupplySummaryQuery>
{
    public GetRequestGoodsSupplySummaryQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
