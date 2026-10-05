using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.CreateProductConsumableVolume;

public record CreateConsumableVolumeProductCommand(
    ProjectOperationDetail ProjectOperationDetail,
    long ProductGroupId,
    decimal? UnusedPercentage,
    bool IsStandard,
    decimal? StandardValue,
    decimal FinalValue,
    VolumeProductType VolumeProductType
    ) : ICommand<ConsumableVolumeProduct>;
