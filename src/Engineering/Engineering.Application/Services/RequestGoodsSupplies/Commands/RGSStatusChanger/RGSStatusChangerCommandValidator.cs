namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSStatusChanger;

public class RGSStatusChangerCommandValidator : AbstractValidator<RGSStatusChangerCommand>
{
    public RGSStatusChangerCommandValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
        RuleFor(oo => oo.Status)
            .IsEnum(RGSCmts.Status);
    }
}