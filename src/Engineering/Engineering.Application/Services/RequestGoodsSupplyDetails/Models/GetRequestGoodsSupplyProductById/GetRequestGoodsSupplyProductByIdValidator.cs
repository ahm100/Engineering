
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;

public class GetRequestGoodsSupplyProductByIdValidator : AbstractValidator<GetRequestGoodsSupplyProductByIdRequest>
{
    public GetRequestGoodsSupplyProductByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
