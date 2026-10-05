
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;

public class GetsWarehouseCategoryByIdQueryValidator : AbstractValidator<GetsWarehouseCategoryByIdQuery>
{
    public GetsWarehouseCategoryByIdQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}
