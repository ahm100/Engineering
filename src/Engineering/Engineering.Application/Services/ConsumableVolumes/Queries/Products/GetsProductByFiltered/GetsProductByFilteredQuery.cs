using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsProductsByFiltered;

public record GetsProductsByFilteredQuery(
    long CostCenterId,
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds
    ) : IQuery<DataResult<List<ConsumableVolumeProduct>>>;
