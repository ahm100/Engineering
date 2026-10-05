using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.DisableProduct;

public class DisableProductCommandHandler : ICommandHandler<DisableProductCommand, ConsumptionStandardProduct>
{
    private readonly ILogger<DisableProductCommand> _logger;
    private readonly IConsumptionStandardProductRepository _repository;

    public DisableProductCommandHandler(ILogger<DisableProductCommand> logger, IConsumptionStandardProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardProduct?>> Handle(DisableProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardProduct>(ProductStandardErrors.ProductWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ConsumptionStandardProduct>(ProductStandardErrors.IsDeleted);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardProduct>(SharedErrors.UnknownError);
        }
    }
}