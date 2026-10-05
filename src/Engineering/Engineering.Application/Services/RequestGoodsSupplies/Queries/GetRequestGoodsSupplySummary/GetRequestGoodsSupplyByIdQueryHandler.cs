using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplySummary;

public class GetRequestGoodsSupplySummaryQueryHandler : IQueryHandler<GetRequestGoodsSupplySummaryQuery, RequestGoodsSupply>
{
    private readonly ILogger<GetRequestGoodsSupplySummaryQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetRequestGoodsSupplySummaryQueryHandler(ILogger<GetRequestGoodsSupplySummaryQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(GetRequestGoodsSupplySummaryQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplySummary(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
