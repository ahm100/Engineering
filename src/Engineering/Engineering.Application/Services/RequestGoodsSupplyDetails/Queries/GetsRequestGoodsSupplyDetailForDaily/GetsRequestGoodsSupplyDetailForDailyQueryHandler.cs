using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailForDaily;

public class GetsRequestGoodsSupplyDetailForDailyQueryHandler : IQueryHandler<GetsRequestGoodsSupplyDetailForDailyQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyDetailForDailyQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsRequestGoodsSupplyDetailForDailyQueryHandler(ILogger<GetsRequestGoodsSupplyDetailForDailyQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsRequestGoodsSupplyDetailForDailyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestGoodsSupplyDetailForDaily(request.ProjectOperationDetailId, ct);

            return result.Data.Any()
              ? new DataResult<List<RequestGoodsSupplyDetail>>
              {
                  Data = result.Data,
                  RowCount = result.RowCount
              }
              : Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailProductsNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
