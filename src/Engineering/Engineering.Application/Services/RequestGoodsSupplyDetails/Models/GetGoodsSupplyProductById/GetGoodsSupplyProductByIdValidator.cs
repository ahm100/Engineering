
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;

public class GetGoodsSupplyProductByIdValidator : AbstractValidator<GetGoodsSupplyProductByIdRequest>
{
    public GetGoodsSupplyProductByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
