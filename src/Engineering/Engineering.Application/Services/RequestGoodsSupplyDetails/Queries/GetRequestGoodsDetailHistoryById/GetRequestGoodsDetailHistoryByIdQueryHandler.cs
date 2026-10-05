using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsDetailHistoryById;

public class GetRequestGoodsDetailHistoryByIdQueryHandler : IQueryHandler<GetRequestGoodsDetailHistoryByIdQuery, DataResult<List<RequestGoodsSupplyDetailHistory>>>
{
    private readonly ILogger<GetRequestGoodsDetailHistoryByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailHistoryRepository _repository;

    public GetRequestGoodsDetailHistoryByIdQueryHandler(ILogger<GetRequestGoodsDetailHistoryByIdQueryHandler> logger, IRequestGoodsSupplyDetailHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetailHistory>>?>> Handle(GetRequestGoodsDetailHistoryByIdQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByRequestGoodsSupplyDetailId(request.Id, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                           ? new DataResult<List<RequestGoodsSupplyDetailHistory>>
                           {
                               Data = entities.Data,
                               RowCount = entities.RowCount
                           } : Result.Failure<DataResult<List<RequestGoodsSupplyDetailHistory>>>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyHistoryWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetailHistory>>>(SharedErrors.UnknownError);
        }
    }
}
