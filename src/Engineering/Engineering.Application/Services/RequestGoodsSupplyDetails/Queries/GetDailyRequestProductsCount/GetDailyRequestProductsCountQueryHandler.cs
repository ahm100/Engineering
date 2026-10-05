using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetDailyRequestProductsCount;

public class GetDailyRequestProductsCountQueryHandler : IQueryHandler<GetDailyRequestProductsCountQuery, decimal?>
{
    private readonly ILogger<GetDailyRequestProductsCountQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetDailyRequestProductsCountQueryHandler(ILogger<GetDailyRequestProductsCountQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<decimal?>> Handle(GetDailyRequestProductsCountQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetDailyRequestProductsCount(ct);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<decimal?>(SharedErrors.UnknownError);
        }
    }
}
