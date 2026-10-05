
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductById;

public class GetGoodsSupplyProductByIdQueryValidator : AbstractValidator<GetGoodsSupplyProductByIdQuery>
{
    public GetGoodsSupplyProductByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
