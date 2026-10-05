using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;


public class GetProductInventoryByFilterQueryHandler : IQueryHandler<GetProductInventoryByFilterQuery, DataResult<List<FilteredInventory>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetProductInventoryByFilterQueryHandler> _logger;

    public GetProductInventoryByFilterQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetProductInventoryByFilterQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<FilteredInventory>>?>> Handle(GetProductInventoryByFilterQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetProductInventoryByFilter(request.Adapt<GetProductInventoryByFilterRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<FilteredInventory>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FilteredInventory>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<FilteredInventory>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
