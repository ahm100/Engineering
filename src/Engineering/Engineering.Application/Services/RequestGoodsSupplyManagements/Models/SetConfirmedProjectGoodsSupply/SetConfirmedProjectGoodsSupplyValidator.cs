
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public class SetConfirmedProjectGoodsSupplyValidator : AbstractValidator<SetConfirmedProjectGoodsSupplyRequest>
{
    public SetConfirmedProjectGoodsSupplyValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
        RuleForEach(c => c.Details).NotEmpty().NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetails).SetValidator(new SetConfirmedProjectGoodsSupplyDetailValidator());
    }
}
