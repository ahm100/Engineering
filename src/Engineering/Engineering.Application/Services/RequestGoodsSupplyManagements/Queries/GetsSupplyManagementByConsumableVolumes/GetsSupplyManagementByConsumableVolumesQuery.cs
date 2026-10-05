using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsSupplyManagementByConsumableVolumes;

public record GetsSupplyManagementByConsumableVolumesQuery(
    List<long> ConsumableVolumeIds
    ) : IQuery<List<RequestGoodsSupplyManagement>>;
