using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductConsumableVolumeById;

public record GetConsumableVolumeProductByIdQuery(
    long Id
    ) : IQuery<ConsumableVolumeProduct>;
