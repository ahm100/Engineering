using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableVolumeProductsForSupply;

public record GetsConsumableVolumeProductsForSupplyQuery(
    VolumeProductType? Type,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? ProductGroupId,
    long? ContractorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumableVolumeProduct>>>;
