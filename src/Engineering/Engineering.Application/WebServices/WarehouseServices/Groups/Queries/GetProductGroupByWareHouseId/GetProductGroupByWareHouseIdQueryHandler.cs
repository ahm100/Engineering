using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public class GetProductGroupByWareHouseIdQueryHandler : IQueryHandler<GetProductGroupByWareHouseIdQuery, DataResult<List<GroupsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetProductGroupByWareHouseIdQueryHandler> _logger;

    public GetProductGroupByWareHouseIdQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetProductGroupByWareHouseIdQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GroupsModel>>?>> Handle(GetProductGroupByWareHouseIdQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetProductGroupByWareHouseId(request.Adapt<GetProductGroupByWareHouseIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GroupsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GroupsModel>>>(SharedErrors.ItemNotFound);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GroupsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
