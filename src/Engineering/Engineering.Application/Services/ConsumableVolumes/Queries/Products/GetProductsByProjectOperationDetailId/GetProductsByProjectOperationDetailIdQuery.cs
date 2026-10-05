using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationDetailId;

public record GetProductsByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumableVolumeProduct>>>;
