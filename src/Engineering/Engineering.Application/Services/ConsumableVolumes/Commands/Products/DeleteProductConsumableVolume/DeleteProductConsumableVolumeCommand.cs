using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.DeleteProductConsumableVolume;

public record DeleteConsumableVolumeProductCommand(
    long Id
    ) : ICommand<ConsumableVolumeProduct>;
