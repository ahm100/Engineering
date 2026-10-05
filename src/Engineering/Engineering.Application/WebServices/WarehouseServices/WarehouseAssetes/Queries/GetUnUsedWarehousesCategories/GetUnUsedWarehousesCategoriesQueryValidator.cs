
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;

public class GetUnUsedWarehousesCategoriesQueryValidator : AbstractValidator<GetUnUsedWarehousesCategoriesQuery>
{
    public GetUnUsedWarehousesCategoriesQueryValidator()
    {
        RuleForEach(c => c.WarehouseIds)
            .IsPositive(GlobalCmts.Id);
    }
}
