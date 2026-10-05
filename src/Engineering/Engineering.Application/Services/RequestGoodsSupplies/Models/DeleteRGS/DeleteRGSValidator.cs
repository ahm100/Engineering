namespace Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;

public class DeleteRGSValidator : AbstractValidator<DeleteRGSRequest>
{
    public DeleteRGSValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}