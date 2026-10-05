using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsManagementHistoryById;

public class GetRequestGoodsManagementHistoryByIdQueryHandler : IQueryHandler<GetRequestGoodsManagementHistoryByIdQuery, DataResult<List<RequestGoodsSupplyManagementHistory>>>
{
    private readonly ILogger<GetRequestGoodsManagementHistoryByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyManagementHistoryRepository _repository;

    public GetRequestGoodsManagementHistoryByIdQueryHandler(ILogger<GetRequestGoodsManagementHistoryByIdQueryHandler> logger, IRequestGoodsSupplyManagementHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyManagementHistory>>?>> Handle(GetRequestGoodsManagementHistoryByIdQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByRequestGoodsSupplyManagementId(request.Id, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                           ? new DataResult<List<RequestGoodsSupplyManagementHistory>>
                           {
                               Data = entities.Data,
                               RowCount = entities.RowCount
                           } : Result.Failure<DataResult<List<RequestGoodsSupplyManagementHistory>>>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyHistoryWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyManagementHistory>>>(SharedErrors.UnknownError);
        }
    }
}
