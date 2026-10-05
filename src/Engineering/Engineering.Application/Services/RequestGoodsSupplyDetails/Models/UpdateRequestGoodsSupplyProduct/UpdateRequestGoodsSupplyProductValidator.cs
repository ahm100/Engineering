
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyProduct;

public class UpdateRequestGoodsSupplyProductValidator : AbstractValidator<UpdateRequestGoodsSupplyProductRequest>
{
    public UpdateRequestGoodsSupplyProductValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
