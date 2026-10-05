using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryById;

public record GetsCostCategoryByIdQuery(
    List<long>? Ids,
    long? GroupId,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCategoryModel>>>;
