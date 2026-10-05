using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ConsumptionStandardProduct>
{
    private readonly ILogger<CreateProductCommand> _logger;
    private readonly IConsumptionStandardProductRepository _repository;

    public CreateProductCommandHandler(ILogger<CreateProductCommand> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardProduct?>> Handle(CreateProductCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumptionStandardProduct(request.OperationInfo, request.ProductUnitId, request.Number, request.UnusedPercentage, request.StandardProductType, request.ProductAllowedType);
            var result = await _repository.Create(entity, ct);
            request.OperationInfo.SetStandard();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardProduct>(SharedErrors.UnknownError);
        }
    }
}