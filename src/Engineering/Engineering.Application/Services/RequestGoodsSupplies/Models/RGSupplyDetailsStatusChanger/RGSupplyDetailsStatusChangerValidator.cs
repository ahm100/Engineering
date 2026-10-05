namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;

public class RGSupplyDetailsStatusChangerValidator : AbstractValidator<RGSupplyDetailsStatusChangerRequest>
{
    public RGSupplyDetailsStatusChangerValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
        RuleFor(oo => oo.Status)
            .IsEnum(RGSCmts.Status);
    }
}