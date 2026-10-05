
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedContractorGoodsSupply;

public class SetConfirmedContractorGoodsSupplyValidator : AbstractValidator<SetConfirmedContractorGoodsSupplyRequest>
{
    public SetConfirmedContractorGoodsSupplyValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
        //RuleForEach(c => c.Details).NotNull().NotEmpty().SetValidator(new SetConfirmedContractorGoodsSupplyDetailValidator());
    }
}
