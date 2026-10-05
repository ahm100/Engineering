
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;

public class UpdateRequestGoodsSupplyValidator : AbstractValidator<UpdateRequestGoodsSupplyRequest>
{
    public UpdateRequestGoodsSupplyValidator()
    {
        RuleFor(oo => oo.OperationInfoSeasonId)
            .IsPositive(GlobalCmts.OperationInfoSeasonId);
        RuleFor(c => c.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
        RuleForEach(c => c.Details)
            .NotEmpty()
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetail)
            .SetValidator(new UpdateRequestGoodsSupplyDetailModelValidator());
    }
}
