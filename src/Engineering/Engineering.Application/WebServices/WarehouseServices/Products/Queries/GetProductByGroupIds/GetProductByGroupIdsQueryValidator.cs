namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetProductByGroupIds;

public class GetProductByGroupIdsQueryValidator : AbstractValidator<GetProductByGroupIdsQuery>
{
    public GetProductByGroupIdsQueryValidator()
    {
        RuleFor(oo => oo.GroupIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
    }
}