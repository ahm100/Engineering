
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForRelocationStatusChenger;

public class GoodsSupplyProductExitForRelocationStatusChengerCommandValidator : AbstractValidator<GoodsSupplyProductExitForRelocationStatusChengerCommand>
{
    public GoodsSupplyProductExitForRelocationStatusChengerCommandValidator()
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
