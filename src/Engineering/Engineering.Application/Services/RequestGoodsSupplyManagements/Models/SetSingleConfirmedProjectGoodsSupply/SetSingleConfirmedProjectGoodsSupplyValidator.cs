
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetSingleConfirmedProjectGoodsSupply;

public class SetSingleConfirmedProjectGoodsSupplyValidator : AbstractValidator<SetSingleConfirmedProjectGoodsSupplyRequest>
{
    public SetSingleConfirmedProjectGoodsSupplyValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
        RuleFor(c => c.Detail).NotEmpty().NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetails).SetValidator(new SetSingleConfirmedProjectGoodsSupplyDetailValidator());
    }
}
