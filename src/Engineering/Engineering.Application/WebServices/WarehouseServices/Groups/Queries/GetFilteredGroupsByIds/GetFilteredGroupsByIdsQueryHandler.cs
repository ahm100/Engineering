using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByIds;

public class GetFilteredGroupsByIdsQueryHandler : IQueryHandler<GetFilteredGroupsByIdsQuery, DataResult<List<FilteredGroup>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetFilteredGroupsByIdsQueryHandler> _logger;

    public GetFilteredGroupsByIdsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetFilteredGroupsByIdsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<FilteredGroup>>?>> Handle(GetFilteredGroupsByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetFilteredGroupsByIds(request.Adapt<GetFilteredGroupsByIdsRequest>(), ct);

            return (result.Value?.Data?.Any()) ?? false ?
                new DataResult<List<FilteredGroup>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<FilteredGroup>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<FilteredGroup>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
