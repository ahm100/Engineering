namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;

public class GetRequestGoodsSupplyProductGroupsRequestValidator : AbstractValidator<GetRequestGoodsSupplyProductGroupsRequest>
{
    public GetRequestGoodsSupplyProductGroupsRequestValidator()
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
