using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupplyDraft;

public class CreateRequestGoodsSupplyDraftValidator : AbstractValidator<CreateRequestGoodsSupplyDraftRequest>
{
    public CreateRequestGoodsSupplyDraftValidator()
    {
        RuleFor(oo => oo.OperationInfoSeasonId)
            .IsPositive(GlobalCmts.OperationInfoSeasonId);
        RuleFor(oo => oo.Type)
            .IsEnum(RequestGoodsSupplyErrors.InValidType);
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleForEach(oo => oo.Details)
            .NotEmpty()
            .SetValidator(new CreateRequestGoodsSupplyDetailModelValidator()).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetail);
    }
}
