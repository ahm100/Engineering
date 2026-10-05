using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyByIdForSeason;

public class GetRequestGoodsSupplyByIdForSeasonQueryHandler : IQueryHandler<GetRequestGoodsSupplyByIdForSeasonQuery, RequestGoodsSupply>
{
    private readonly ILogger<GetRequestGoodsSupplyByIdForSeasonQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetRequestGoodsSupplyByIdForSeasonQueryHandler(ILogger<GetRequestGoodsSupplyByIdForSeasonQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(GetRequestGoodsSupplyByIdForSeasonQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyByIdForSeason(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
