using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsProductByOperationInfoId;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;


namespace Engineering.Application.Services.ConsumptionStandards.Queries.Product.GetsProductsByOperationInfoId;

public class GetsProductByOperationInfoIdQueryHandler : IQueryHandler<GetsProductByOperationInfoIdQuery, DataResult<List<ConsumptionStandardProduct>>>
{
    private readonly IConsumptionStandardProductRepository _repository;
    private readonly ILogger<GetsProductByOperationInfoIdQueryHandler> _logger;

    public GetsProductByOperationInfoIdQueryHandler(ILogger<GetsProductByOperationInfoIdQueryHandler> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumptionStandardProduct>>?>> Handle(GetsProductByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.ProductGetByOprationInfoId(request.OprationInfoId, request.StandardProductType, request.ProductAllowedType, request.PageIndex, request.PageSize, ct);

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