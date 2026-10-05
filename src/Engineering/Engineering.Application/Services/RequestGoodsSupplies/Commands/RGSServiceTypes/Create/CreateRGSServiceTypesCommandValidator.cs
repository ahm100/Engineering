namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Create;

public class CreateRGSServiceTypesCommandValidator : AbstractValidator<CreateRGSServiceTypesCommand>
{
    public CreateRGSServiceTypesCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .IsEntity(RGSCmts.RequestGoodsSupply);
    }
}