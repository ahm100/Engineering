using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProjectOperationId;

public class GetsGoodsSupplyDetailByProjectOperationIdQueryHandler : IQueryHandler<GetsGoodsSupplyDetailByProjectOperationIdQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsGoodsSupplyDetailByProjectOperationIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsGoodsSupplyDetailByProjectOperationIdQueryHandler(ILogger<GetsGoodsSupplyDetailByProjectOperationIdQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsGoodsSupplyDetailByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsGoodsSupplyDetailByProjectOperationId(request.ProjectOperationId, 0, 0, ct);

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
