
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;

public class GetRequestGoodsSupplyByIdValidator : AbstractValidator<GetRequestGoodsSupplyByIdRequest>
{
    public GetRequestGoodsSupplyByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
