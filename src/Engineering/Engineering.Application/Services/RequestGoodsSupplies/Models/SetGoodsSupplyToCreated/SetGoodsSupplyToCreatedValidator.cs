
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.SetGoodsSupplyToCreated;

public class SetGoodsSupplyToCreatedValidator : AbstractValidator<SetGoodsSupplyToCreatedRequest>
{
    public SetGoodsSupplyToCreatedValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
