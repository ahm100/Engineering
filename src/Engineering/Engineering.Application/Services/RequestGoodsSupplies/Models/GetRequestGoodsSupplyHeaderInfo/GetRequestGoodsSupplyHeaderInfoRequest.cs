namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyHeaderInfo;

public record GetRequestGoodsSupplyHeaderInfoRequest(
    long CostCenterId,
    long ProjectId,
    long ProjectOperationId,
    long? ProjectOperationDetailId
    ) : IHttpRequest;