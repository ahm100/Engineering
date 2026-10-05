using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProductDetailByRequestId;

public class GetsProductDetailByRequestIdQueryHandler : IQueryHandler<GetsProductDetailByRequestIdQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetsProductDetailByRequestIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsProductDetailByRequestIdQueryHandler(ILogger<GetsProductDetailByRequestIdQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetsProductDetailByRequestIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProductDetailByRequestId(request.RequestGoodsSupplyId, ct);

            return result.Data.Any()
              ? new DataResult<List<RequestGoodsSupplyDetail>>
              {
                  Data = result.Data,
                  RowCount = result.RowCount
              }
              : Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
