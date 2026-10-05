
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.SetOperationInfoSeasonToGoodsSupply;

public class SetOperationInfoSeasonToGoodsSupplyValidator : AbstractValidator<SetOperationInfoSeasonToGoodsSupplyRequest>
{
    public SetOperationInfoSeasonToGoodsSupplyValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.SeasonId)
            .IsPositive(GlobalCmts.SeasonId);
    }
}
