namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGS;

public class DeleteRGSCommandValidator : AbstractValidator<DeleteRGSCommand>
{
    public DeleteRGSCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}