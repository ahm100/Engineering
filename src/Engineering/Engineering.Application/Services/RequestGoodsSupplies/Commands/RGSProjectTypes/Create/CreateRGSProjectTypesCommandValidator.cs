namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Create;

public class CreateRGSProjectTypesCommandValidator : AbstractValidator<CreateRGSProjectTypesCommand>
{
    public CreateRGSProjectTypesCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .IsEntity(RGSCmts.RequestGoodsSupply);
    }
}