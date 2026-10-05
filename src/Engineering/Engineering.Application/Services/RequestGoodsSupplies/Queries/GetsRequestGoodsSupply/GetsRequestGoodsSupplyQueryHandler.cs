using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetsRequestGoodsSupply;

public class GetsRequestGoodsSupplyQueryHandler : IQueryHandler<GetsRequestGoodsSupplyQuery, DataResult<List<RequestGoodsSupply>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetsRequestGoodsSupplyQueryHandler(ILogger<GetsRequestGoodsSupplyQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupply>>?>> Handle(GetsRequestGoodsSupplyQuery request, CT ct)
    {
        try
        {
            var requestGoods = await _repository.GetsRequestGoodsSupply(request.CostCenterIds, request.ProjectIds, request.CityId, request.ProjectManagerId, request.ProjectOperationId, request.ProjectOperationDetailId,
                request.FilterData, request.CreatorId, request.Statuses, request.RemoveStatuses, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = requestGoods.Data.Any()
                ? new DataResult<List<RequestGoodsSupply>>
                {
                    Data = requestGoods.Data,
                    RowCount = requestGoods.RowCount
                }
                : Result.Failure<DataResult<List<RequestGoodsSupply>>>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupply>>>(SharedErrors.UnknownError);
        }
    }
}
