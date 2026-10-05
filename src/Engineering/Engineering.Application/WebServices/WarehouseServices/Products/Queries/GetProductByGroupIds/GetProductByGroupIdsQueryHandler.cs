using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetProductByGroupIds;

public class GetProductByGroupIdsQueryHandler : IQueryHandler<GetProductByGroupIdsQuery, DataResult<List<GetProductsByGroupIdsModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetProductByGroupIdsQueryHandler> _logger;

    public GetProductByGroupIdsQueryHandler(ILogger<GetProductByGroupIdsQueryHandler> logger, IWarehouseService productService)
    {
        _logger = logger;
        _warehouseService = productService;
    }

    public async Task<Result<DataResult<List<GetProductsByGroupIdsModel>>?>> Handle(GetProductByGroupIdsQuery request, CT ct)
    {
        try
        {
            var req = new GetProductByGroupIdsRequest(request.GroupIds, request.FilterData);
            var result = await _warehouseService.GetProductByGroupIds(request.Adapt<GetProductByGroupIdsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetProductsByGroupIdsModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetProductsByGroupIdsModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetProductsByGroupIdsModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
