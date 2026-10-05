using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using ConsumableVolumeProduct = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeProduct;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableProductForSupply;

public record GetsConsumableProductForSupplyQuery(
    VolumeProductType? Type,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? ProductGroupId,
    long? ContractorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumableVolumeProduct>>>;
