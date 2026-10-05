using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.UpdateProductConsumableVolume;

public record UpdateConsumableVolumeProductCommand(
    ConsumableVolumeProduct ConsumableVolumeProduct,
    long ProductGroupId,
    decimal? UnusedPercentage,
    bool IsStandard,
    decimal? StandardValue,
    decimal FinalValue,
    VolumeProductType VolumeProductType
    ) : ICommand<ConsumableVolumeProduct>;
