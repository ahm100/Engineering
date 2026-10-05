
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.AddGoodsSupplyDetailManagementDescription;

public class AddGoodsSupplyDetailManagementDescriptionValidator : AbstractValidator<AddGoodsSupplyDetailManagementDescriptionRequest>
{
    public AddGoodsSupplyDetailManagementDescriptionValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
