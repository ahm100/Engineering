using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;

public record GetWarehouseCategoryByIdQuery(
    long Id
    ) : IQuery<CategoryModel?>;
