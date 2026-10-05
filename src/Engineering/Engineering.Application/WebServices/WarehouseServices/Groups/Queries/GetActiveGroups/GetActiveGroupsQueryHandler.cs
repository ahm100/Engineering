using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetActiveGroups;

public class GetActiveGroupsQueryHandler : IQueryHandler<GetActiveGroupsQuery, DataResult<List<ActiveGroupsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetActiveGroupsQueryHandler> _logger;

    public GetActiveGroupsQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetActiveGroupsQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<ActiveGroupsModel>>?>> Handle(GetActiveGroupsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetActiveGroups(request.Adapt<GetActiveGroupsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<ActiveGroupsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<ActiveGroupsModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<ActiveGroupsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
