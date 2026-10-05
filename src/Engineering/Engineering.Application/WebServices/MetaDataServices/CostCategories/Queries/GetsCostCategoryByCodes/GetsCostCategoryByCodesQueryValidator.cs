
namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryByCodes;

public class GetsCostCategoryByCodesQueryValidator : AbstractValidator<GetsCostCategoryByCodesQuery>
{
    public GetsCostCategoryByCodesQueryValidator()
    {
        RuleFor(oo => oo.Codes).NotEmpty().WithError(MetaDataErrors.CodeIsEmpty);
    }
}
