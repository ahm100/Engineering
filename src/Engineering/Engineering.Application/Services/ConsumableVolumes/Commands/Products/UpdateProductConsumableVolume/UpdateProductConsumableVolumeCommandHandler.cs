using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.UpdateProductConsumableVolume;

public class UpdateConsumableVolumeProductCommandHandler : ICommandHandler<UpdateConsumableVolumeProductCommand, ConsumableVolumeProduct>
{
    private readonly ILogger<UpdateConsumableVolumeProductCommand> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public UpdateConsumableVolumeProductCommandHandler(ILogger<UpdateConsumableVolumeProductCommand> logger,
        IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeProduct?>> Handle(UpdateConsumableVolumeProductCommand request, CT ct)
    {
        try
        {
            var entity = request.ConsumableVolumeProduct;
            entity.SetFinalValue(request.FinalValue);
            entity.SetProductGroupId(request.ProductGroupId);
            entity.SetUnusedPercentage(request.UnusedPercentage);
            entity.SetIsStandard(request.IsStandard);
            entity.SetStandardValue(request.StandardValue);
            entity.SetType(request.VolumeProductType);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumableVolumeProduct>(SharedErrors.UnknownError);
        }
    }
}
