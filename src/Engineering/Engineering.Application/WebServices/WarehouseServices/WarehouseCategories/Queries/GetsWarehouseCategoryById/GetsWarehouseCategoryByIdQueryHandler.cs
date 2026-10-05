using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetsWarehouseCategoryById;
using CategoryModel = Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.WarehouseCategory;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;

public class GetsWarehouseCategoryByIdQueryHandler : IQueryHandler<GetsWarehouseCategoryByIdQuery, DataResult<List<CategoryModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetsWarehouseCategoryByIdQueryHandler> _logger;

    public GetsWarehouseCategoryByIdQueryHandler(ILogger<GetsWarehouseCategoryByIdQueryHandler> logger, IWarehouseService repository)
    {
        _logger = logger;
        _warehouseService = repository;
    }

    public async Task<Result<DataResult<List<CategoryModel>>?>> Handle(GetsWarehouseCategoryByIdQuery request, CT ct)
    {
        try
        {
            var ids = request.Ids.Where(x => x.HasValue).Select(x => x.Value).ToList();
            var req = new GetsWarehouseCategoryByIdRequest
            {
                Ids = ids,
                IgnoreQuery = request.IgnoreQuery,
                FilterData = request.FilterData,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            var result = await _warehouseService.GetsWarehouseCategoryById(req, ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<CategoryModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<CategoryModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CategoryModel>>>(SharedErrors.UnknownError);
        }
    }
}
