using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetail;

public class GetsRequestGoodsSupplyDetailQueryHandler : IQueryHandler<GetsRequestGoodsSupplyDetailQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyDetailQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsRequestGoodsSupplyDetailQueryHandler(ILogger<GetsRequestGoodsSupplyDetailQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsRequestGoodsSupplyDetailQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestGoodsSupplyDetail(request.Id, request.Statuses, request.RemoveStatuses, request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = new DataResult<List<RequestGoodsSupplyDetail>>()
            {
                Data = result.Data ?? new List<RequestGoodsSupplyDetail>(0),
                RowCount = result.RowCount
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
