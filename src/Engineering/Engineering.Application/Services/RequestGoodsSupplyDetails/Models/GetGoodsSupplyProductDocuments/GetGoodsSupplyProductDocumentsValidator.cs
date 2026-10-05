
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductDocuments;

public class GetGoodsSupplyProductDocumentsValidator : AbstractValidator<GetGoodsSupplyProductDocumentsRequest>
{
    public GetGoodsSupplyProductDocumentsValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
