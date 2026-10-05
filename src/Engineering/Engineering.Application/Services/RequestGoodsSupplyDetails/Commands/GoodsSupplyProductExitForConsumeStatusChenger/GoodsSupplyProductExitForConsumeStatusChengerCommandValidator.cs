
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForConsumeStatusChenger;

public class GoodsSupplyProductExitForConsumeStatusChengerCommandValidator : AbstractValidator<GoodsSupplyProductExitForConsumeStatusChengerCommand>
{
    public GoodsSupplyProductExitForConsumeStatusChengerCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.InvoiceId)
            .IsPositive(GlobalCmts.InvoiceId);
        RuleFor(c => c.WarehouseId)
            .IsPositive(GlobalCmts.WarehouseId);
        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
