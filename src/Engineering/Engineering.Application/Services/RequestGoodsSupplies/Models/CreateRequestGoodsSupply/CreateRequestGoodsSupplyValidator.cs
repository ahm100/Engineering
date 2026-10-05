using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;

public class CreateRequestGoodsSupplyValidator : AbstractValidator<CreateRequestGoodsSupplyRequest>
{
    public CreateRequestGoodsSupplyValidator()
    {
        RuleFor(oo => oo.OperationInfoSeasonId)
            .IsPositive(GlobalCmts.OperationInfoSeasonId);
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleForEach(oo => oo.Details)
            .NotEmpty()
            .SetValidator(new CreateRequestGoodsSupplyDetailModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetail);
    }
}
