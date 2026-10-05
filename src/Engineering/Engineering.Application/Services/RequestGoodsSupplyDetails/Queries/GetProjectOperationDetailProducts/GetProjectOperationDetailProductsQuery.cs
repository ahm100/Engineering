using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailProducts;

public record GetProjectOperationDetailProductsQuery(
    long ProjectOperationDetailId,
    long? ProductGroupId
    ) : IQuery<List<ConsumableVolumeProduct>>;
