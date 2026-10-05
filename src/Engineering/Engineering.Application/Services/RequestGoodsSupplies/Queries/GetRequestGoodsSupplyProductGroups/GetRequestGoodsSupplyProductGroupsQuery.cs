using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProductGroups;

public record GetRequestGoodsSupplyProductGroupsQuery(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    VolumeProductType? ProductType
    ) : IQuery<DataResult<List<RequestGoodsSupply>>>;
