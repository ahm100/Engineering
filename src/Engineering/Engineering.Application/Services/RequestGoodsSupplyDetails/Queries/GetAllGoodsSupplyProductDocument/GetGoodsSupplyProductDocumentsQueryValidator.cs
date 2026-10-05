
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetAllGoodsSupplyProductDocument;

public class GetAllGoodsSupplyProductDocumentQueryValidator : AbstractValidator<GetAllGoodsSupplyProductDocumentQuery>
{
    public GetAllGoodsSupplyProductDocumentQueryValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
