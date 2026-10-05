using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsHistoryById;

public class GetRequestGoodsHistoryByIdQueryHandler : IQueryHandler<GetRequestGoodsHistoryByIdQuery, DataResult<List<RequestGoodsSupplyHistory>>>
{
    private readonly ILogger<GetRequestGoodsHistoryByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyHistoryRepository _repository;

    public GetRequestGoodsHistoryByIdQueryHandler(ILogger<GetRequestGoodsHistoryByIdQueryHandler> logger, IRequestGoodsSupplyHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyHistory>>?>> Handle(GetRequestGoodsHistoryByIdQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByRequestGoodsSupplyId(request.Id, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                           ? new DataResult<List<RequestGoodsSupplyHistory>>
                           {
                               Data = entities.Data,
                               RowCount = entities.RowCount
                           } : Result.Failure<DataResult<List<RequestGoodsSupplyHistory>>>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyHistoryWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyHistory>>>(SharedErrors.UnknownError);
        }
    }
}
