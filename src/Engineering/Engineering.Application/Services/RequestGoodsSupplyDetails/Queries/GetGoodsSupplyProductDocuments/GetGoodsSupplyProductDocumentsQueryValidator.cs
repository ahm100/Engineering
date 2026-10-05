
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductDocuments;

public class GetGoodsSupplyProductDocumentsQueryValidator : AbstractValidator<GetGoodsSupplyProductDocumentsQuery>
{
    public GetGoodsSupplyProductDocumentsQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
