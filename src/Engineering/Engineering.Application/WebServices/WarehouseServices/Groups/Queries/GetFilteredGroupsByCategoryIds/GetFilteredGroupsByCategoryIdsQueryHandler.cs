using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByCategoryIds;

public class GetFilteredGroupsByCategoryIdsQueryHandler : IQueryHandler<GetFilteredGroupsByCategoryIdsQuery, DataResult<List<FilteredGroupsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetFilteredGroupsByCategoryIdsQueryHandler> _logger;

    public GetFilteredGroupsByCategoryIdsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetFilteredGroupsByCategoryIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<FilteredGroupsModel>>?>> Handle(GetFilteredGroupsByCategoryIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetFilteredGroupsByCategoryIds(request.Adapt<GetFilteredGroupsByCategoryIdsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<FilteredGroupsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FilteredGroupsModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<FilteredGroupsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
