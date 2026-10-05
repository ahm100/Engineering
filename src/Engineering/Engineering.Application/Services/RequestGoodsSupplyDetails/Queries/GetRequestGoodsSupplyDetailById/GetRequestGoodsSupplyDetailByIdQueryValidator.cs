
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailById;

public class GetRequestGoodsSupplyDetailByIdQueryValidator : AbstractValidator<GetRequestGoodsSupplyDetailByIdQuery>
{
    public GetRequestGoodsSupplyDetailByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
