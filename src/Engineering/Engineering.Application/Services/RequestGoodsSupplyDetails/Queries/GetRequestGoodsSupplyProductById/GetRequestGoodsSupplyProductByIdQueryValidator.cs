
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductById;

public class GetRequestGoodsSupplyProductByIdQueryValidator : AbstractValidator<GetRequestGoodsSupplyProductByIdQuery>
{
    public GetRequestGoodsSupplyProductByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
