using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRGPForManagerProductIds;

public class GetRGPForManagerProductIdsQueryHandler : IQueryHandler<GetRGPForManagerProductIdsQuery, List<long>>
{
    private readonly ILogger<GetRGPForManagerProductIdsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetRGPForManagerProductIdsQueryHandler(
        ILogger<GetRGPForManagerProductIdsQueryHandler> logger,
        IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetRGPForManagerProductIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRGPForManagerProductIds(ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<long>>(SharedErrors.UnknownError);
        }
    }
}
