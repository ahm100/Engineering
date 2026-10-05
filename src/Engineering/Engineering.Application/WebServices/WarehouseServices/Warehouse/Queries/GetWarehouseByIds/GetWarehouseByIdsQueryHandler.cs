using Engineering.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;

public class GetWarehouseByIdsQueryHandler : IQueryHandler<GetWarehouseByIdsQuery, DataResult<List<GetWarehouseByIdsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetWarehouseByIdsQueryHandler> _logger;

    public GetWarehouseByIdsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetWarehouseByIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetWarehouseByIdsModel>>?>> Handle(GetWarehouseByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetWarehouseByIds(request.Adapt<GetWarehouseByIdsRequest>(), ct);

            return (result.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetWarehouseByIdsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetWarehouseByIdsModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetWarehouseByIdsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
