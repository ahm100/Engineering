using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ConsumptionStandardProduct>
{
    private readonly ILogger<UpdateProductCommand> _logger;
    private readonly IConsumptionStandardProductRepository _repository;

    public UpdateProductCommandHandler(ILogger<UpdateProductCommand> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardProduct?>> Handle(UpdateProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardProduct>(ProductStandardErrors.ProductWithIdNotFound);

            entity.SetProductUnitId(request.ProductUnitId);
            entity.SetStandardProductType(request.StandardProductType);
            entity.SetProductAllowedType(request.ProductAllowedType);
            entity.SetNumber(request.Number);
            entity.SetUnusedPercentage(request.UnusedPercentage);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ConsumptionStandardProduct>(SharedErrors.UnknownError);
        }
    }
}