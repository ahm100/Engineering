using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryByCodes;

public record GetsCostCategoryByCodesQuery(
    List<string>? Codes
    ) : IQuery<DataResult<List<CostCategoryModel>>>;
