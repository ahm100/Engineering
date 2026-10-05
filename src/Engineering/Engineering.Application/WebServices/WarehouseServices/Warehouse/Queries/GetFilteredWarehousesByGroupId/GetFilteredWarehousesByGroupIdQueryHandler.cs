using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;

public class GetFilteredWarehousesByGroupIdQueryHandler : IQueryHandler<GetFilteredWarehousesByGroupIdQuery, DataResult<List<GetFilteredWarehousesByGroupIdModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetFilteredWarehousesByGroupIdQueryHandler> _logger;

    public GetFilteredWarehousesByGroupIdQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetFilteredWarehousesByGroupIdQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredWarehousesByGroupIdModel>>?>> Handle(GetFilteredWarehousesByGroupIdQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetFilteredWarehousesByGroupId(request.Adapt<GetFilteredWarehousesByGroupIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetFilteredWarehousesByGroupIdModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetFilteredWarehousesByGroupIdModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetFilteredWarehousesByGroupIdModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
