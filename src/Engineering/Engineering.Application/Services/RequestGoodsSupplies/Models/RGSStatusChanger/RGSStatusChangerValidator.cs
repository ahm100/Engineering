namespace Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;

public class RGSStatusChangerValidator : AbstractValidator<RGSStatusChangerRequest>
{
    public RGSStatusChangerValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
        RuleFor(oo => oo.Status)
            .IsEnum(RGSCmts.Status);
    }
}