using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetWarehouseCategoryById;
using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;

public class GetWarehouseCategoryByIdQueryHandler : IQueryHandler<GetWarehouseCategoryByIdQuery, CategoryModel?>
{
    private readonly ILogger<GetWarehouseCategoryByIdQueryHandler> _logger;
    private readonly IWarehouseService _warehouseService;

    public GetWarehouseCategoryByIdQueryHandler(ILogger<GetWarehouseCategoryByIdQueryHandler> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    public async Task<Result<CategoryModel?>> Handle(GetWarehouseCategoryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetWarehouseCategoryById(request.Adapt<GetWarehouseCategoryByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CategoryModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CategoryModel?>(SharedErrors.UnknownError);
        }
    }
}
