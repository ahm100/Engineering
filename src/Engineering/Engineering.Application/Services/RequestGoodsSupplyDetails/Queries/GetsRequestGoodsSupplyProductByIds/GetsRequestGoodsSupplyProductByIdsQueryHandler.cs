using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProductByIds;

public class GetsRequestGoodsSupplyProductByIdsQueryHandler : IQueryHandler<GetsRequestGoodsSupplyProductByIdsQuery, DataResult<List<RequestGoodsSupplyProduct>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyProductByIdsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetsRequestGoodsSupplyProductByIdsQueryHandler(ILogger<GetsRequestGoodsSupplyProductByIdsQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyProduct>>?>> Handle(GetsRequestGoodsSupplyProductByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestGoodsSupplyProductByIds(request.Ids, 1, request.Ids.Count(), ct);

            var response = new DataResult<List<RequestGoodsSupplyProduct>>()
            {
                Data = result.Data ?? new List<RequestGoodsSupplyProduct>(0),
                RowCount = result.RowCount
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyProduct>>>(SharedErrors.UnknownError);
        }
    }
}
