using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProductByIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;
using ProductModel = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Models.Product;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProductByIds;

public class GetsFilteredProductByIdsQueryHandler : IQueryHandler<GetsFilteredProductByIdsQuery, DataResult<List<ProductModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetsFilteredProductByIdsQueryHandler> _logger;

    public GetsFilteredProductByIdsQueryHandler(ILogger<GetsFilteredProductByIdsQueryHandler> logger, IWarehouseService productService)
    {
        _logger = logger;
        _warehouseService = productService;
    }

    public async Task<Result<DataResult<List<ProductModel>>?>> Handle(GetsFilteredProductByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetsFilteredProductByIds(request.Adapt<GetsFilteredProductByIdsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<ProductModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<ProductModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<ProductModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
