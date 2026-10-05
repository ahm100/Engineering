
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProductDetailByRequestId;

public class GetsProductDetailByRequestIdQueryValidator : AbstractValidator<GetsProductDetailByRequestIdQuery>
{
    public GetsProductDetailByRequestIdQueryValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
    }
}