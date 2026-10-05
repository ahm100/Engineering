using Engineering.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;

public class GetUnUsedWarehousesCategoriesQueryHandler : IQueryHandler<GetUnUsedWarehousesCategoriesQuery, DataResult<List<GetUnUsedWarehousesCategoriesModel>?>?>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetUnUsedWarehousesCategoriesQueryHandler> _logger;

    public GetUnUsedWarehousesCategoriesQueryHandler(
        ILogger<GetUnUsedWarehousesCategoriesQueryHandler> logger,
        IWarehouseService repository)
    {
        _logger = logger;
        _warehouseService = repository;
    }

    public async Task<Result<DataResult<List<GetUnUsedWarehousesCategoriesModel>?>?>> Handle(GetUnUsedWarehousesCategoriesQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetUnUsedWarehousesCategories(request, ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<GetUnUsedWarehousesCategoriesModel>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetUnUsedWarehousesCategoriesModel>?>>(SharedErrors.ItemNotFound);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetUnUsedWarehousesCategoriesModel>>>(WarehouseErrors.ProviderError(rsponse?.Error.Message!))!;
        }
    }
}
