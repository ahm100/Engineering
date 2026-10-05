using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetDailyRequestProvidedProductsCount;

public class GetDailyRequestProvidedProductsCountQueryHandler : IQueryHandler<GetDailyRequestProvidedProductsCountQuery, decimal?>
{
    private readonly ILogger<GetDailyRequestProvidedProductsCountQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetDailyRequestProvidedProductsCountQueryHandler(ILogger<GetDailyRequestProvidedProductsCountQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<decimal?>> Handle(GetDailyRequestProvidedProductsCountQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetDailyRequestProvidedProductsCount(ct);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<decimal?>(SharedErrors.UnknownError);
        }
    }
}
