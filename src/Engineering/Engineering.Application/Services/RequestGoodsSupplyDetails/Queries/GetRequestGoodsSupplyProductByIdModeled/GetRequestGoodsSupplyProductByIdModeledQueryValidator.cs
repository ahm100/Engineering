
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductByIdModeled;

public class GetRequestGoodsSupplyProductByIdModeledQueryValidator : AbstractValidator<GetRequestGoodsSupplyProductByIdModeledQuery>
{
    public GetRequestGoodsSupplyProductByIdModeledQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
