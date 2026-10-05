
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.AddGoodsSupplyDetailManagementDescription;

public class AddGoodsSupplyDetailManagementDescriptionCommandValidator : AbstractValidator<AddGoodsSupplyDetailManagementDescriptionCommand>
{
    public AddGoodsSupplyDetailManagementDescriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
