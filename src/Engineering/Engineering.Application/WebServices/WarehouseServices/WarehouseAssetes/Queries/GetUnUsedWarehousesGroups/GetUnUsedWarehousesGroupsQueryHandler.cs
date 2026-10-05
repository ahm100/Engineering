using Engineering.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;

public class GetUnUsedWarehousesGroupsQueryHandler : IQueryHandler<GetUnUsedWarehousesGroupsQuery, DataResult<List<GetUnUsedWarehousesGroupsModel>?>?>
{
    private readonly IWarehouseService _metaDataService;
    private readonly ILogger<GetUnUsedWarehousesGroupsQueryHandler> _logger;

    public GetUnUsedWarehousesGroupsQueryHandler(
        ILogger<GetUnUsedWarehousesGroupsQueryHandler> logger,
        IWarehouseService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<GetUnUsedWarehousesGroupsModel>?>?>> Handle(GetUnUsedWarehousesGroupsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetUnUsedWarehousesGroups(request, ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<GetUnUsedWarehousesGroupsModel>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetUnUsedWarehousesGroupsModel>?>>(SharedErrors.ItemNotFound);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetUnUsedWarehousesGroupsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!))!;
        }
    }
}
