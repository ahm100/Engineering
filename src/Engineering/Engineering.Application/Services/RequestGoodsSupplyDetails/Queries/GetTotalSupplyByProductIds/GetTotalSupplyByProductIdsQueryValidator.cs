
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProductIds;

public class GetTotalSupplyByProductIdsQueryValidator : AbstractValidator<GetTotalSupplyByProductIdsQuery>
{
    public GetTotalSupplyByProductIdsQueryValidator()
    {
        RuleFor(oo => oo.ProductIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
    }
}
