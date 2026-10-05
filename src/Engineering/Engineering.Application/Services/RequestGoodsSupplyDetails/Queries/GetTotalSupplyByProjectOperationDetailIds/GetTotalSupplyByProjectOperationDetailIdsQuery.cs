using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProjectOperationDetailIds;

public record GetTotalSupplyByProjectOperationDetailIdsQuery(
    List<long>? ProjectOperationDetailIds,
    long ProductGroupId
    ) : IQuery<List<GetTotalSupplyByProjectOperationDetailIdsModel>>;
