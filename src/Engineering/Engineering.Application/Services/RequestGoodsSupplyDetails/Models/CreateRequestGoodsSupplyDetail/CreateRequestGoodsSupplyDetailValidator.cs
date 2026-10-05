namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

public class CreateRequestGoodsSupplyDetailValidator : AbstractValidator<CreateRequestGoodsSupplyDetailModelRequest>
{
    public CreateRequestGoodsSupplyDetailValidator()
    {
        RuleForEach(c => c.Details).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId).SetValidator(new CreateRequestGoodsSupplyDetailModelValidator());
    }
}
