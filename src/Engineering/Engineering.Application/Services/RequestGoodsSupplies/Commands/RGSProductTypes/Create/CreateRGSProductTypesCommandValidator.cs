namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Create;

public class CreateRGSProductTypesCommandValidator : AbstractValidator<CreateRGSProductTypesCommand>
{
    public CreateRGSProductTypesCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .IsEntity(RGSCmts.RequestGoodsSupply);
    }
}