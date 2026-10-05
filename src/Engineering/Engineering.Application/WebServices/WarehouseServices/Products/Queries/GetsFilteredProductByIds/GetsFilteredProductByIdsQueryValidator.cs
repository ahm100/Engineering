
namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProductByIds;

public class GetsFilteredProductByIdsQueryValidator : AbstractValidator<GetsFilteredProductByIdsQuery>
{
    public GetsFilteredProductByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}
