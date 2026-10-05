using Engineering.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetsMainWarehouseIds;

public class GetsMainWarehouseIdsQueryHandler : IQueryHandler<GetsMainWarehouseIdsQuery, DataResult<List<long>?>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetsMainWarehouseIdsQueryHandler> _logger;

    public GetsMainWarehouseIdsQueryHandler(
        IWarehouseService warehouseService,
        ILogger<GetsMainWarehouseIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<long>?>?>> Handle(GetsMainWarehouseIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetsMainWarehouseIds(ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<long>?>
                {
                    Data = result.Value.Data,
                } : Result.Failure<DataResult<List<long>?>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<long>?>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
