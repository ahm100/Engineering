using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetCostCategoryById;

public record GetCostCategoryByIdQuery(
    long Id
    ) : IQuery<CostCategoryModel?>;
