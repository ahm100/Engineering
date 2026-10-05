using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupIds;

public class GetFilteredWarehousesByGroupIdsQueryHandler : IQueryHandler<GetFilteredWarehousesByGroupIdsQuery, DataResult<List<GetFilteredWarehousesByGroupIdsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetFilteredWarehousesByGroupIdsQueryHandler> _logger;

    public GetFilteredWarehousesByGroupIdsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetFilteredWarehousesByGroupIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredWarehousesByGroupIdsModel>>?>> Handle(GetFilteredWarehousesByGroupIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetFilteredWarehousesByGroupIds(request.Adapt<GetFilteredWarehousesByGroupIdsRequest>(), ct);

            return (result.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetFilteredWarehousesByGroupIdsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetFilteredWarehousesByGroupIdsModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetFilteredWarehousesByGroupIdsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
