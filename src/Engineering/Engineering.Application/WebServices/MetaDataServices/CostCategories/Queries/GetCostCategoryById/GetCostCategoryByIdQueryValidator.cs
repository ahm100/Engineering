
namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetCostCategoryById;

public class GetCostCategoryByIdQueryValidator : AbstractValidator<GetCostCategoryByIdQuery>
{
    public GetCostCategoryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}