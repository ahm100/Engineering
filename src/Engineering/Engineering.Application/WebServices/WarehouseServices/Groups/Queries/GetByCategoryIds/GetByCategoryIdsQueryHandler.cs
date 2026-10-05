using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetByCategoryIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public class GetByCategoryIdsQueryHandler : IQueryHandler<GetGroupsByCategoryIdsQuery, DataResult<List<GetFilteredGroupsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetByCategoryIdsQueryHandler> _logger;

    public GetByCategoryIdsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetByCategoryIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredGroupsModel>>?>> Handle(GetGroupsByCategoryIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetByCategoryIds(request.Adapt<GetGroupByCategoryIdsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetFilteredGroupsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetFilteredGroupsModel>>>(SharedErrors.ItemNotFound);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetFilteredGroupsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
