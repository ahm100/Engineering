using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductHistoryById;

public class GetGoodsSupplyProductHistoryByIdQueryHandler : IQueryHandler<GetGoodsSupplyProductHistoryByIdQuery, DataResult<List<RequestGoodsSupplyProductHistory>>>
{
    private readonly ILogger<GetGoodsSupplyProductHistoryByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductHistoryRepository _repository;

    public GetGoodsSupplyProductHistoryByIdQueryHandler(ILogger<GetGoodsSupplyProductHistoryByIdQueryHandler> logger, IRequestGoodsSupplyProductHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyProductHistory>>?>> Handle(GetGoodsSupplyProductHistoryByIdQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetGoodsSupplyProductHistoryById(request.Id, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                           ? new DataResult<List<RequestGoodsSupplyProductHistory>>
                           {
                               Data = entities.Data,
                               RowCount = entities.RowCount
                           } : Result.Failure<DataResult<List<RequestGoodsSupplyProductHistory>>>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyProductHistory>>>(SharedErrors.UnknownError);
        }
    }
}
