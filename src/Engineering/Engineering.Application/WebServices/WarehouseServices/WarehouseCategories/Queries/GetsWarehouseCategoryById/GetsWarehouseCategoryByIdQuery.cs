using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;

public record GetsWarehouseCategoryByIdQuery(
    int PageIndex,
    int PageSize,
    List<long?> Ids,
    bool IgnoreQuery,
    string? FilterData
    ) : IQuery<DataResult<List<CategoryModel>>>;
