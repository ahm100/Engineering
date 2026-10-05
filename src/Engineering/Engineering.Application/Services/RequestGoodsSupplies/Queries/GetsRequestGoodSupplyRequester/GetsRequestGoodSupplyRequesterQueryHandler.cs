using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetsRequestGoodSupplyRequester;

public class GetsRequestGoodSupplyRequesterQueryHandler : IQueryHandler<GetsRequestGoodSupplyRequesterQuery, DataResult<List<long>>>
{
    private readonly ILogger<GetsRequestGoodSupplyRequesterQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetsRequestGoodSupplyRequesterQueryHandler(ILogger<GetsRequestGoodSupplyRequesterQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsRequestGoodSupplyRequesterQuery request, CT ct)
    {
        try
        {
            var requestGoods = await _repository.GetsRequestGoodSupplyRequester(ct);

            var response = requestGoods.Any()
                ? new DataResult<List<long>>
                {
                    Data = requestGoods
                }
                : Result.Failure<DataResult<List<long>>>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
