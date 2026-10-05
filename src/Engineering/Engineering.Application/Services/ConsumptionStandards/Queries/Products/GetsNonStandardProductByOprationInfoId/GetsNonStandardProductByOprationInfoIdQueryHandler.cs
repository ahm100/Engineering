using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsNonStandardProductByOprationInfoId;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;


namespace Engineering.Application.Services.ConsumptionStandards.Queries.Product.GetsNonStandardProductByOprationInfoId;

public class GetsNonStandardProductByOprationInfoIdQueryHandler : IQueryHandler<GetsNonStandardProductByOprationInfoIdQuery, DataResult<List<ConsumptionStandardProduct>>>
{
    private readonly IConsumptionStandardProductRepository _repository;
    private readonly ILogger<GetsNonStandardProductByOprationInfoIdQueryHandler> _logger;

    public GetsNonStandardProductByOprationInfoIdQueryHandler(ILogger<GetsNonStandardProductByOprationInfoIdQueryHandler> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumptionStandardProduct>>?>> Handle(GetsNonStandardProductByOprationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.NonStandardProductGetByOprationInfoId(request.OprationInfoId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumptionStandardProduct>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumptionStandardProduct>>>(ProductStandardErrors.ProductNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumptionStandardProduct>>>(SharedErrors.UnknownError);
        }
    }
}