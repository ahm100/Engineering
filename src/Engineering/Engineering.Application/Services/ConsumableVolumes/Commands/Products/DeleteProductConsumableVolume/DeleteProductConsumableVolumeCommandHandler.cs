using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.DeleteProductConsumableVolume;

public class DeleteConsumableVolumeProductCommandHandler : ICommandHandler<DeleteConsumableVolumeProductCommand, ConsumableVolumeProduct>
{
    private readonly ILogger<DeleteConsumableVolumeProductCommand> _logger;
    private readonly IConsumableVolumeProductRepository _repository;

    public DeleteConsumableVolumeProductCommandHandler(ILogger<DeleteConsumableVolumeProductCommand> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumableVolumeProduct?>> Handle(DeleteConsumableVolumeProductCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumableVolumeProduct>(ConsumableVolumeProductErrors.ProjectOperationDetailWithIdNotFound);
            if (entity.IsStandard)
                return Result.Failure<ConsumableVolumeProduct>(ConsumableVolumeProductErrors.DataIsStandard);
            if (entity.RequestGoodsSupplyDetails.Count > 0)
                return Result.Failure<ConsumableVolumeProduct>(ConsumableVolumeProductErrors.HaveRequestGoodsSupplyDetails);

            entity.SetIsDeleted();
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
