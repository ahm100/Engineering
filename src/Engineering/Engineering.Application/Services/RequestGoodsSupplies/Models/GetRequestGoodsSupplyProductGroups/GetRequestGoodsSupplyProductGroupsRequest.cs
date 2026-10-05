using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;

public record GetRequestGoodsSupplyProductGroupsRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    VolumeProductType? ProductType,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
