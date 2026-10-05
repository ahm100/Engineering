
namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;

public class GetRequestGoodsSupplyByIdQueryValidator : AbstractValidator<GetRequestGoodsSupplyByIdQuery>
{
    public GetRequestGoodsSupplyByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
