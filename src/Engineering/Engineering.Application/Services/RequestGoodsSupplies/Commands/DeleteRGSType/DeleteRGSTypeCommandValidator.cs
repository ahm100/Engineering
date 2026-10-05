namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGSType;

public class DeleteRGSTypeCommandValidator : AbstractValidator<DeleteRGSTypeCommand>
{
    public DeleteRGSTypeCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}