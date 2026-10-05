
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRequestGoodsSupply;

public class DeleteRequestGoodsSupplyValidator : AbstractValidator<DeleteRequestGoodsSupplyRequest>
{
    public DeleteRequestGoodsSupplyValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}