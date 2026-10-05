using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProductId;

public class GetsGoodsSupplyDetailByProductIdQueryHandler : IQueryHandler<GetsGoodsSupplyDetailByProductIdQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsGoodsSupplyDetailByProductIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsGoodsSupplyDetailByProductIdQueryHandler(ILogger<GetsGoodsSupplyDetailByProductIdQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsGoodsSupplyDetailByProductIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsGoodsSupplyDetailByProductId(request.Id, request.PageIndex, request.PageSize, ct);

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
