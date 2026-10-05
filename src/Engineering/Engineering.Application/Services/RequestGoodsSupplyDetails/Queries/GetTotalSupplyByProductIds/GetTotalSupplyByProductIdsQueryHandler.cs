using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProductIds;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProductIds;

public class GetTotalSupplyByProductIdsQueryHandler : IQueryHandler<GetTotalSupplyByProductIdsQuery, List<GetTotalSupplyByProductIdsModel>>
{
    private readonly ILogger<GetTotalSupplyByProductIdsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetTotalSupplyByProductIdsQueryHandler(ILogger<GetTotalSupplyByProductIdsQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetTotalSupplyByProductIdsModel>?>> Handle(GetTotalSupplyByProductIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetTotalSupplyByProductIdsAsync(request.ProductIds, ct);

            return result.Any() ? result : new List<GetTotalSupplyByProductIdsModel>(0);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<GetTotalSupplyByProductIdsModel>>(SharedErrors.UnknownError);
        }
    }
}
