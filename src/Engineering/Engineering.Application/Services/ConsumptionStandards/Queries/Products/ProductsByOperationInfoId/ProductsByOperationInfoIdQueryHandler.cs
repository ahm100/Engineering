using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;


namespace Engineering.Application.Services.ConsumptionStandards.Queries.Products.ProductsByOperationInfoId;

public class ProductsByOperationInfoIdQueryHandler : IQueryHandler<ProductsByOperationInfoIdQuery, DataResult<List<ConsumptionStandardProduct>>>
{
    private readonly IConsumptionStandardProductRepository _repository;
    private readonly ILogger<ProductsByOperationInfoIdQueryHandler> _logger;

    public ProductsByOperationInfoIdQueryHandler(ILogger<ProductsByOperationInfoIdQueryHandler> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumptionStandardProduct>>?>> Handle(ProductsByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.ProductByOprationInfoId(request.OprationInfoId, ct);

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