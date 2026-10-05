using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailByIds;

public class GetsRequestGoodsSupplyDetailByIdsQueryHandler : IQueryHandler<GetsRequestGoodsSupplyDetailByIdsQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyDetailByIdsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsRequestGoodsSupplyDetailByIdsQueryHandler(ILogger<GetsRequestGoodsSupplyDetailByIdsQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsRequestGoodsSupplyDetailByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestGoodsSupplyDetailByIds(request.Ids, ct);

            var response = new DataResult<List<RequestGoodsSupplyDetail>>()
            {
                Data = result.Data ?? new List<RequestGoodsSupplyDetail>(0),
                RowCount = result.RowCount
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
