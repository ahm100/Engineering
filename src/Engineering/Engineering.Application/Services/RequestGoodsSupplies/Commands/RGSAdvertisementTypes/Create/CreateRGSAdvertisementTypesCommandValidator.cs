namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Create;

public class CreateRGSAdvertisementTypesCommandValidator : AbstractValidator<CreateRGSAdvertisementTypesCommand>
{
    public CreateRGSAdvertisementTypesCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .IsEntity(RGSCmts.RequestGoodsSupply);
    }
}