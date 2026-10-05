using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetGroupsForGoodsProduct;
using Gita.Backend.Shared.Domain.Errors.WebServices;
namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetGroupsForGoodsProduct;


public class GetGroupsForGoodsProductQueryHandler : IQueryHandler<GetGroupsForGoodsProductQuery, DataResult<List<GetGroupsForGoodsProductModel>>>
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<GetGroupsForGoodsProductQueryHandler> _logger;

    public GetGroupsForGoodsProductQueryHandler(IWarehouseService warehouseService,
                                ILogger<GetGroupsForGoodsProductQueryHandler> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetGroupsForGoodsProductModel>>?>> Handle(GetGroupsForGoodsProductQuery request, CT ct)
    {
        try
        {
            var result = await _warehouseService.GetGroupsForGoodsSupplyByCategoryIds(request.Adapt<GetGroupsForGoodsProductRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetGroupsForGoodsProductModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetGroupsForGoodsProductModel>>>(SharedErrors.ItemNotFound);

        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var rsponse = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            return Result.Failure<DataResult<List<GetGroupsForGoodsProductModel>>>(WarehouseErrors.ProviderError(rsponse!.Error.Message!));
        }
    }
}
