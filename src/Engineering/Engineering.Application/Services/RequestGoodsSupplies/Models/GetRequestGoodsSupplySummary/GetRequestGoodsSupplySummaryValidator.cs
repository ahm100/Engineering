
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplySummary;

public class GetRequestGoodsSupplySummaryValidator : AbstractValidator<GetRequestGoodsSupplySummaryRequest>
{
    public GetRequestGoodsSupplySummaryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
