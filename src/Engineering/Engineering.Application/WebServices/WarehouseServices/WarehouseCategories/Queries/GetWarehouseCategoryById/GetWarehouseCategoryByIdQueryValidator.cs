
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;

public class GetWarehouseCategoryByIdQueryValidator : AbstractValidator<GetWarehouseCategoryByIdQuery>
{
    public GetWarehouseCategoryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}