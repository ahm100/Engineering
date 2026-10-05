namespace Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGSType;

public class DeleteRGSTypeValidator : AbstractValidator<DeleteRGSTypeRequest>
{
    public DeleteRGSTypeValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}