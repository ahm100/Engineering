using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProducts;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProducts;

public class GetsFilteredProductsQueryHandler : IQueryHandler<GetsFilteredProductsQuery, DataResult<List<long>?>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetsFilteredProductsQueryHandler> _logger;

    public GetsFilteredProductsQueryHandler(ILogger<GetsFilteredProductsQueryHandler> logger, IWarehouseService productService)
    {
        _logger = logger;
        _warehouseService = productService;
    }

    public async Task<Result<DataResult<List<long>?>?>> Handle(GetsFilteredProductsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetsFilteredProducts(request.Adapt<GetsFilteredProductsRequest>(), ct);

            return (result?.Value?.Ids?.Any()) ?? false ?
                new DataResult<List<long>?>
                {
                    Data = result.Value.Ids,
                    RowCount = result.Value.RowCount
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
